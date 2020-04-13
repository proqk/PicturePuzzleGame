using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;
using System.IO;

/*
 * 스테이지가 시작할 때 각각 박스의 이미지 6개를 가져온다
 * 정답인 이미지 인덱스를 저장한다
 * 오답이면 정답 빼고 5개 이미지를 1. 새로 뽑아서 2. 위치를 바꾼다
 */
public class GameManager3 : MonoBehaviour
{
    public int answer, nowStage; //정답, 현재 스테이지
    int answer_text;
    public GameObject questionImage, boxtext, boxback; //문제 이미지 오브젝트, 박스들
    Text[] boxText; //문제 텍스트
    Image[] boxBack; //문제 박스
    public GameObject O, X; //O, X
    float[,] mixposition;
    float[,] mixposition_1 = new float[2, 3] { { 0, -105, 0 }, { 0, -351, 0 } }; //자리 섞기 좌표
    float[,] mixposition_2 = new float[3, 3] { { 0, -72, 0 }, { 0, -254, 0 }, { 0, -439, 0 } }; //자리 섞기 좌표
    public csvReader csvreader; //csv파일 읽는 스크립트
    List<Tuple<string, int>> data; //스테이지와 정답 리스트
    List<BoxManager> bm = new List<BoxManager>(); //박스매니저 리스트
    public ButtonManager ButtonManager; //마지막 스테이지에 도달하면 뒤로 가기 버튼과 동일
    public tts tts;
    int nowLevel, difficulty; //현재 레벨, 난이도(버튼 몇 개인지)
    string stageLock;

    void Start()
    {
        data = csvreader.Read("symbolstage"); //스테이지 정보 불러옴

        nowStage = GameObject.Find("LevelSelector").GetComponent<StageButton>().stageNum;
        nowLevel = GameObject.Find("whatlevel").GetComponent<StageButton>().level;
        boxText = boxtext.GetComponentsInChildren<Text>(); //박스 텍스트들
        boxBack = boxback.GetComponentsInChildren<Image>(); //박스 이미지들

        if (nowLevel == 1)
        {
            difficulty = 2;
            stageLock = "stage3level1Reached";
            mixposition = mixposition_1;
        }
        else if (nowLevel == 2)
        {
            difficulty = 3;
            stageLock = "stage3level2Reached";
            mixposition = mixposition_2;
        }

        O.SetActive(false);
        X.SetActive(false);
        for(int i = 0; i < boxText.Length; i++) //일단 다 끈다
        {
            boxText[i].gameObject.SetActive(false);
            boxBack[i].gameObject.SetActive(false);
        }

        for(int i = 0; i < difficulty; i++) //난이도 별로 박스 활성화, 정답 넣기
        {
            boxBack[i].gameObject.SetActive(true);
            boxText[i].gameObject.SetActive(true);

            boxBack[i].transform.localPosition = new Vector3(mixposition[i, 0], mixposition[i, 1], mixposition[i, 2]);
            boxText[i].transform.localPosition = new Vector3(mixposition[i, 0], mixposition[i, 1], mixposition[i, 2]);

            boxText[i].gameObject.GetComponent<BoxManager>().me = difficulty + 1;
            bm.Add(boxText[i].gameObject.GetComponent<BoxManager>());
        }

        this.q();
    }

    List<int> GetnewText()
    {
        List<int> num = new List<int>();

        while (num.Count < difficulty)
        {
            int tmp = Random.Range(0, data.Count);
            //중복 아님/전체 인덱스를 넘지 않는 수/정답이랑 겹치지 않는 수
            if (!num.Contains(tmp) && tmp < data.Count && tmp != data[nowStage].Item2)
            {
                num.Add(tmp);
            }
        }
        return num;
    } //전체 데이터에서 랜덤수 difficulty개 뽑음

    void SetAllnewText()
    {
        List<int> newTextIndex = new List<int>();
        newTextIndex = GetnewText();

        for (int i = 0; i < difficulty; i++)
        {
            bm[i].gameObject.GetComponent<Text>().text = data[newTextIndex[i]].Item1;
        }
    }//텍스트를 각 box에 붙임

    public void recallStage(int n)
    {
        X.SetActive(true);
        StartCoroutine(WaitAndPrint());
        this.q();
    } //틀렸을 때 스테이지 다시 불러오기

    public void correct()
    {
        O.SetActive(true);
        StartCoroutine(WaitAndPrint());
    } //맞았을 때 다음 스테이지로

    IEnumerator AvoidFastClick() //선택지를 누르고 O/X가 뜨는 중에는 아무것도 클릭못함
    {
        for (int i = 0; i < difficulty; i++)
        {
            boxText[i].gameObject.GetComponent<Button>().interactable = false;
        }
        yield return new WaitForSeconds(1); //1초 대기
    }

    IEnumerator WaitAndPrint()
    {
        yield return AvoidFastClick();
        for (int i = 0; i < difficulty; i++)
        {
            boxText[i].gameObject.GetComponent<Button>().interactable = true; //1초 대기 후에는 클릭할 수 있어야 함
        }

        if (X.activeSelf == true)
        {
            X.SetActive(false);
        }
        else
        {
            O.SetActive(false);
            nowStage += 1; //맞으면 다음 스테이지로 자동 이동
            int openStage = PlayerPrefs.GetInt(stageLock);
            if (openStage < nowStage) //최대 깬 스테이지보다 전 스테이지면 갱신하면 안 됨, 더 클 때만 갱신
            {
                PlayerPrefs.SetInt(stageLock, nowStage); //현재 스테이지를 깨면 스테이지락 해제
            }
            if (nowStage == data.Count) //만약 마지막 스테이지라면 스테이지 선택 창으로 돌아감
            {
                ButtonManager.Scene3ToStage3();
            }
            else this.q(); //그렇지 않다면 다음 스테이지로    
        }
    } //O,X출력 후 1초 대기

    public void textread() //문제를 읽는다
    {
        tts.readText(data[nowStage-1].Item1);
    }

    public void q() //스테이지 시작
    {
        Sprite newImage = Resources.Load<Sprite>(Path.Combine("symbol_Images/", data[nowStage].Item2.ToString()));
        questionImage.gameObject.GetComponent<Image>().sprite = newImage; //문제 이미지 바꿈
        this.SetAllnewText(); //전체 이미지를 새로 뽑는다

        //정답 텍스트를 붙인다-어차피 섞을 것
        bm[0].gameObject.GetComponent<Text>().text = data[nowStage-1].Item1;
        answer = 1; //1번 박스가 정답이다

        //위치를 전체 섞는다
        this.mix();
    }


    void mix()
    {
        int[] Imageshuffle= new int[difficulty];
        int tmp;

        for (int i = 0; i < difficulty; i++) Imageshuffle[i] = i; //배열에 섞을 값을 넣는다
        for (int i = 0; i < Imageshuffle.Length; i++) //배열 숫자를 섞는다
        {
            int rnd = Random.Range(0, Imageshuffle.Length);
            tmp = Imageshuffle[rnd];
            Imageshuffle[rnd] = Imageshuffle[i];
            Imageshuffle[i] = tmp;
        }
        this.answer = Imageshuffle[answer - 1]; //정답 업데이트

        for (int i = 0; i < difficulty; i++)
        {
            this.setmix(Imageshuffle[i], boxText[i]); //섞인 숫자 번호의 좌표로 이미지를 옮긴다
            bm[i].me = Imageshuffle[i];
        }
    } //텍스트 섞음


    void setmix(int index, Text text)
    {
        float x = mixposition[index, 0];
        float y = mixposition[index, 1];
        float z = mixposition[index, 2];
        //image.transform.localPosition = new Vector3(x, y, z); //자식 객체 이동은 localPosition
        RectTransform rt = (RectTransform)text.transform; //UI이동은 recttransform
        rt.anchoredPosition = new Vector3(x, y, z);
    } //이미지의 위치를 섞는다
}