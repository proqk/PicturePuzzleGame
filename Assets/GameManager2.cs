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
public class GameManager2 : MonoBehaviour
{
    public int answer, nowStage; //정답, 현재 스테이지
    int answer_image;
    public GameObject boximage, boxback;
    Image[] boxBack, boxImage; //이모티콘이 표시될 오브젝트
    public GameObject O, X; //O, X
    float[,] mixposition;
    float[,] mixposition_1 = new float[3, 3] { { -220, -211, 0 }, { 0, -211, 0 }, { 220, -211, 0 } };
    float[,] mixposition_2 = new float[6, 3] { { -220, -70, 0 }, { 0, -70, 0 }, { 220, -70, 0 }, { -220, -335, 0 }, { 0, -335, 0 }, { 220, -335, 0 } }; //자리 섞기 좌표
    Transform[] questBox; //질문 박스(자식 전체)
    public Text question; //문제 텍스트
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
        boxImage = boximage.GetComponentsInChildren<Image>(); //박스 이미지들
        boxBack = boxback.GetComponentsInChildren<Image>(); //박스 뒷배경 이미지들

        if (nowLevel == 1)
        {
            difficulty = 3;
            stageLock = "stage2level1Reached";
            mixposition = mixposition_1;
        }
        else if (nowLevel == 2)
        {
            difficulty = 6;
            stageLock = "stage2level2Reached";
            mixposition = mixposition_2;
        }

        O.SetActive(false);
        X.SetActive(false);

        for (int i = 0; i < boxImage.Length; i++) //일단 다 끈다
        {
            boxImage[i].gameObject.SetActive(false);
            boxBack[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < difficulty; i++) //난이도 별로 박스 활성화, 정답 넣기
        {
            boxBack[i].gameObject.SetActive(true);
            boxImage[i].gameObject.SetActive(true);

            boxBack[i].transform.localPosition = new Vector3(mixposition[i, 0], mixposition[i, 1], mixposition[i, 2]);
            boxImage[i].transform.localPosition = new Vector3(mixposition[i, 0], mixposition[i, 1], mixposition[i, 2]);

            boxImage[i].gameObject.GetComponent<BoxManager>().me = difficulty + 1;
            bm.Add(boxImage[i].gameObject.GetComponent<BoxManager>());
        }

        this.q();
    }

    List<Sprite> GetnewImage()
    {

        List<int> num = new List<int>();
        List<Sprite> imgBox = new List<Sprite>();

        while (num.Count < difficulty)
        {
            int tmp = Random.Range(0, data.Count);
            //중복 아님/전체 인덱스를 넘지 않는 수/정답이랑 겹치지 않는 수/리소스 파일에 그 이름이 있는지 확인
            if (!num.Contains(tmp) && tmp < data.Count && tmp != data[nowStage].Item2
                /*&& File.Exists(Path.Combine("Assets/Resources/symbol_Images/", tmp.ToString() + ".png")) 왠지 이거 안드에선 안 됨*/)
            {
                Sprite newImage = Resources.Load<Sprite>(Path.Combine("symbol_Images/", tmp.ToString()));
                if (newImage)
                {
                    imgBox.Add(newImage); //실제 이미지 배열
                    num.Add(tmp); //중복 체크를 위한 숫자 배열
                }
            }
        }

       return imgBox;
    } //각 이미지 박스에서 새 이미지 뽑음

    void SetAllnewImage()
    {
        List<Sprite> newImageIndex = new List<Sprite>();
        newImageIndex = GetnewImage();

            for (int i = 0; i < difficulty; i++)
        {
            bm[i].gameObject.GetComponent<Image>().sprite = newImageIndex[i];
        }
    }//새 이미지 뽑힌 걸로 각 box에 붙임

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
            boxImage[i].gameObject.GetComponent<Button>().interactable = false;
        }
        yield return new WaitForSeconds(1); //1초 대기
    }
    IEnumerator WaitAndPrint()
    {
        yield return AvoidFastClick();
        for (int i = 0; i < difficulty; i++)
        {
            boxImage[i].gameObject.GetComponent<Button>().interactable = true; //1초 대기 후에는 클릭할 수 있어야 함
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
                ButtonManager.Scene2ToStage2();
            }
            else this.q(); //그렇지 않다면 다음 스테이지로    
        }
    } //O,X출력 후 1초 대기

    public void textread() //문제를 읽는다
    {
        tts.readText(data[nowStage].Item1);
    }

    public void q() //스테이지 시작
    {
        if (nowLevel == 1) PlayerPrefs.SetInt("stage2level1lastStage", nowStage); //마지막 푼 스테이지 저장
        if (nowLevel == 2) PlayerPrefs.SetInt("stage2level2lastStage", nowStage);

        question.text = data[nowStage].Item1; //문제 텍스트 바꾸기, 텍스트는 0부터 시작
        answer_image = data[nowStage].Item2; //문제 정답 이미지 인덱스 저장, 이미지 이름은 1부터 시작
        this.SetAllnewImage(); //전체 이미지를 새로 뽑는다

        //정답인 이미지를 위에 붙인다(어차피 섞을 거라 1번에다 붙여도ㄱㅊ)
        Sprite answerImage = Resources.Load<Sprite>(Path.Combine("symbol_images/", answer_image.ToString()));
        bm[0].gameObject.GetComponent<Image>().sprite = answerImage;
        answer = 1; //1번 박스가 정답이다

        //위치를 전체 섞는다
        this.mix();
    }

    void mix()
    {
        int[] Imageshuffle = new int[difficulty];
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
            this.setmix(Imageshuffle[i], boxImage[i]); //섞인 숫자 번호의 좌표로 이미지를 옮긴다
            bm[i].me = Imageshuffle[i];
        }
    } //이미지를 섞는다

    void setmix(int index, Image image)
    {
        float x = mixposition[index, 0];
        float y = mixposition[index, 1];
        float z = mixposition[index, 2];
        //image.transform.localPosition = new Vector3(x, y, z); //자식 객체 이동은 localPosition
        RectTransform rt = (RectTransform)image.transform; //UI이동은 recttransform
        rt.anchoredPosition = new Vector3(x, y, z);
    } //6개 이미지의 위치를 섞는다
}