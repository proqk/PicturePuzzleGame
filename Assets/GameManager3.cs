using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

/*
 * 스테이지가 시작할 때 각각 박스의 이미지 6개를 가져온다
 * 정답인 이미지 인덱스를 저장한다
 * 오답이면 정답 빼고 5개 이미지를 1. 새로 뽑아서 2. 위치를 바꾼다
 */
public class GameManager3 : MonoBehaviour
{
    public int answer, nowStage; //정답, 현재 스테이지
    int answer_text;
    GameObject StageButtonClone;
    Sprite[] box; //심볼 박스
    public GameObject questionImage; //문제 이미지 오브젝트
    public Text box1Text, box2Text, box3Text; //문제 텍스트들
    public GameObject O, X; //O, X
    float[,] mixposition = new float[3, 3] { { 0, -110, 0 }, { 0, -280, 0 }, { 0, -450, 0 } }; //자리 섞기 좌표
    //float[,] mixposition = new float[6, 3] { { -220, -70, 0 }, { 0, -70, 0 }, { 220, -70, 0 }, { -220, -335, 0 }, { 0, -335, 0 }, { 220, -335, 0 } }; //자리 섞기 좌표
    public csvReader csvreader; //csv파일 읽는 스크립트
    List<Tuple<string, int>> data; //스테이지와 정답 리스트
    List<BoxManager> bm; //박스매니저 리스트
    public ButtonManager ButtonManager; //마지막 스테이지에 도달하면 뒤로 가기 버튼과 동일
    public tts tts;

    void Start()
    {
        data = csvreader.Read("symbolstage"); //스테이지 정보 불러옴

        StageButtonClone = GameObject.Find("LevelSelector");
        nowStage = StageButtonClone.GetComponent<StageButton>().stageNum;

        O.SetActive(false);
        X.SetActive(false);
        box = Resources.LoadAll<Sprite>("symbol_Images");
        Array.Sort(box, delegate(Sprite x, Sprite y) {return int.Parse(x.name).CompareTo(int.Parse(y.name)); });

        box1Text.GetComponent<BoxManager>().me = 1;
        box2Text.GetComponent<BoxManager>().me = 2;
        box3Text.GetComponent<BoxManager>().me = 3;

        bm = new List<BoxManager>();
        bm.Add(box1Text.GetComponent<BoxManager>());
        bm.Add(box2Text.GetComponent<BoxManager>());
        bm.Add(box3Text.GetComponent<BoxManager>());

        this.q();
    }

    List<int> GetnewText()
    {
        //범위가 너무 커서 뽑는 시간이 많이 걸림
        //전체 데이터에서 값 하나 뽑고, 값+30 사이에서 3개를 뽑음
        //중복이면 다시 뽑음

        List<int> num = new List<int>();
        int randomIndex = Random.Range(data[0].Item2, data[data.Count-1].Item2-30);
        print("랜덤값: "+randomIndex);
        //끝값은 본인 제외한 범위, +50했을 때 넘어가면 안 됨

        while (num.Count <= 3)
        {
            int tmp = Random.Range(data[randomIndex].Item2, data[randomIndex].Item2+30);

            if (!num.Contains(tmp) && tmp < data.Count)
            {
                num.Add(tmp);
            }
        }
        return num;
    } //전체 데이터에서 랜덤수 3개 뽑음

    /*List<int> GetnewText()
    {
        List<int> num = new List<int>();
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(data[0].Item2, data[data.Count].Item2);
            num.Add(randomIndex);
        }
        //while (num.Count <= 3)
        //{
        //    int tmp = Random.Range(data[randomIndex].Item2, data[randomIndex].Item2+6);

        //    if (!num.Contains(tmp))
        //    {
        //        num.Add(tmp);
        //    }
        //}
        return num;
    } //전체 데이터에서 랜덤수 3개 뽑음*/

    void SetAllnewText()
    {
        List<int> newTextIndex = new List<int>();
        newTextIndex = GetnewText();

        for (int i = 0; i < 3; i++)
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
        box1Text.GetComponent<Button>().interactable = false;
        box2Text.GetComponent<Button>().interactable = false;
        box3Text.GetComponent<Button>().interactable = false;
        //box4Image.GetComponent<Button>().interactable = false;
        //box5Image.GetComponent<Button>().interactable = false;
        //box6Image.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(1); //1초 대기
    }

    IEnumerator WaitAndPrint()
    {
        yield return AvoidFastClick();
        box1Text.GetComponent<Button>().interactable = true; //1초 대기 후에는 클릭할 수 있어야 함
        box2Text.GetComponent<Button>().interactable = true;
        box3Text.GetComponent<Button>().interactable = true;
        //box4Image.GetComponent<Button>().interactable = true;
        //box5Image.GetComponent<Button>().interactable = true;
        //box6Image.GetComponent<Button>().interactable = true;

        if (X.activeSelf == true)
        {
            X.SetActive(false);
        }
        else
        {
            O.SetActive(false);
            nowStage += 1; //맞으면 다음 스테이지로 자동 이동
            PlayerPrefs.SetInt("stage3levelReached", nowStage); //현재 스테이지를 깨면 스테이지락 해제
            if (nowStage == data.Count) //만약 마지막 스테이지라면 스테이지 선택 창으로 돌아감
            {
                ButtonManager.Scene3ToStage3();
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
        questionImage.gameObject.GetComponent<Image>().sprite = box[data[nowStage].Item2]; //문제 이미지 바꿈
        this.SetAllnewText(); //전체 이미지를 새로 뽑는다

        //정답 텍스트를 붙인다-어차피 섞을 것
        bm[0].gameObject.GetComponent<Text>().text = data[nowStage].Item1;
        answer = 1; //1번 박스가 정답이다
         
        //위치를 전체 섞는다
        this.mix();
    }

    //void mix()
    //{
    //    int[] Imageshuffle = { 0, 1, 2, 3, 4, 5 };
    //    int tmp;
    //    for (int i = 0; i < Imageshuffle.Length; i++) //배열 숫자를 섞는다
    //    {
    //        int rnd = Random.Range(0, Imageshuffle.Length);
    //        tmp = Imageshuffle[rnd];
    //        Imageshuffle[rnd] = Imageshuffle[i];
    //        Imageshuffle[i] = tmp;
    //    }
    //    this.answer = Imageshuffle[answer - 1]; //정답 업데이트

    //    this.setmix(Imageshuffle[0], box1Text); //섞인 숫자 번호의 좌표로 이미지를 옮긴다
    //    this.setmix(Imageshuffle[1], box2Text);
    //    this.setmix(Imageshuffle[2], box3Text);
    //    //this.setmix(Imageshuffle[3], box4Image);
    //    //this.setmix(Imageshuffle[4], box5Image);
    //    //this.setmix(Imageshuffle[5], box6Image);

    //    bm[0].me = Imageshuffle[0];
    //    bm[1].me = Imageshuffle[1];
    //    bm[2].me = Imageshuffle[2];
    //    //bm[3].me = Imageshuffle[3];
    //    //bm[4].me = Imageshuffle[4];
    //    //bm[5].me = Imageshuffle[5];
    //} //6개 이미지의 위치를 섞는다 //6개 텍스트 섞음

    void mix()
    {
        int[] Imageshuffle = { 0, 1, 2 };
        int tmp;
        for (int i = 0; i < Imageshuffle.Length; i++) //배열 숫자를 섞는다
        {
            int rnd = Random.Range(0, Imageshuffle.Length);
            tmp = Imageshuffle[rnd];
            Imageshuffle[rnd] = Imageshuffle[i];
            Imageshuffle[i] = tmp;
        }
        this.answer = Imageshuffle[answer - 1]; //정답 업데이트

        this.setmix(Imageshuffle[0], box1Text); //섞인 숫자 번호의 좌표로 이미지를 옮긴다
        this.setmix(Imageshuffle[1], box2Text);
        this.setmix(Imageshuffle[2], box3Text);
        //this.setmix(Imageshuffle[3], box4Image);
        //this.setmix(Imageshuffle[4], box5Image);
        //this.setmix(Imageshuffle[5], box6Image);

        bm[0].me = Imageshuffle[0];
        bm[1].me = Imageshuffle[1];
        bm[2].me = Imageshuffle[2];
        //bm[3].me = Imageshuffle[3];
        //bm[4].me = Imageshuffle[4];
        //bm[5].me = Imageshuffle[5];
    } //3개 텍스트 섞음


    void setmix(int index, Text text)
    {
        float x = mixposition[index, 0];
        float y = mixposition[index, 1];
        float z = mixposition[index, 2];
        //image.transform.localPosition = new Vector3(x, y, z); //자식 객체 이동은 localPosition
        RectTransform rt = (RectTransform)text.transform; //UI이동은 recttransform
        rt.anchoredPosition = new Vector3(x, y, z);
    } //6개 이미지의 위치를 섞는다
}