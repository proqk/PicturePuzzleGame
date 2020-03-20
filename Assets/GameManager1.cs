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
public class GameManager1 : MonoBehaviour
{
    public int answer, nowStage; //정답, 현재 스테이지
    int answer_image;
    GameObject StageManager;
    Sprite[] box1, box2, box3, box4, box5, box6; //이모티콘 박스
    public GameObject box1Image, box2Image, box3Image, box4Image, box5Image, box6Image; //이모티콘이 표시될 오브젝트
    public GameObject O, X; //O, X
    float[,] mixposition = new float[6, 3] { { -220, -70, 0 }, { 0, -70, 0 }, { 220, -70, 0 }, { -220, -335, 0 }, { 0, -335, 0 }, { 220, -335, 0 } }; //자리 섞기 좌표
    Transform[] questBox; //질문 박스(자식 전체)
    public Text question; //문제 텍스트
    public csvReader csvreader; //csv파일 읽는 스크립트
    List<Tuple<string, int>> data; //스테이지와 정답 리스트
    List<BoxManager> bm; //박스매니저 리스트
    public ButtonManager ButtonManager; //마지막 스테이지에 도달하면 뒤로 가기 버튼과 동일
    public TextToSpeech tts;

    void Start()
    {
        data = csvreader.Read("stage"); //스테이지 정보 불러옴

        StageManager = GameObject.Find("LevelSelector");
        nowStage = StageManager.GetComponent<StageButton>().stageNum;

        O.SetActive(false);
        X.SetActive(false);
        box1 = Resources.LoadAll<Sprite>("1_LikeLove");
        box2 = Resources.LoadAll<Sprite>("2_Fighting");
        box3 = Resources.LoadAll<Sprite>("3_FunHi");
        box4 = Resources.LoadAll<Sprite>("4_SadNice");
        box5 = Resources.LoadAll<Sprite>("5_Sorry");
        box6 = Resources.LoadAll<Sprite>("6_Active");

        box1Image.GetComponent<BoxManager>().me = 1;
        box2Image.GetComponent<BoxManager>().me = 2;
        box3Image.GetComponent<BoxManager>().me = 3;
        box4Image.GetComponent<BoxManager>().me = 4;
        box5Image.GetComponent<BoxManager>().me = 5;
        box6Image.GetComponent<BoxManager>().me = 6;

        bm = new List<BoxManager>(); //박스 6개로 초기화
        bm.Add(box1Image.GetComponent<BoxManager>());
        bm.Add(box2Image.GetComponent<BoxManager>());
        bm.Add(box3Image.GetComponent<BoxManager>());
        bm.Add(box4Image.GetComponent<BoxManager>());
        bm.Add(box5Image.GetComponent<BoxManager>());
        bm.Add(box6Image.GetComponent<BoxManager>());

        this.SetAllnewImage();
        this.q();
    }

    public Sprite GetnewImage(int num)
    {
        int randomQuestionIndex;
        if (num == 1)
        {
            randomQuestionIndex = Random.Range(0, box1.Length);
            return box1[randomQuestionIndex];
        }
        else if (num == 2)
        {
            randomQuestionIndex = Random.Range(0, box2.Length);
            return box2[randomQuestionIndex];
        }
        else if (num == 3)
        {
            randomQuestionIndex = Random.Range(0, box3.Length);
            return box3[randomQuestionIndex];
        }
        else if (num == 4)
        {
            randomQuestionIndex = Random.Range(0, box4.Length);
            return box4[randomQuestionIndex];
        }
        else if (num == 5)
        {
            randomQuestionIndex = Random.Range(0, box5.Length);
            return box5[randomQuestionIndex];
        }
        else if (num == 6)
        {
            randomQuestionIndex = Random.Range(0, box6.Length);
            return box6[randomQuestionIndex];
        }
        return null;
    } //각 이미지 박스에서 새 이미지 뽑음

    public void SetAllnewImage()
    {
        bm[0].gameObject.GetComponent<Image>().sprite = GetnewImage(1);
        bm[1].gameObject.GetComponent<Image>().sprite = GetnewImage(2);
        bm[2].gameObject.GetComponent<Image>().sprite = GetnewImage(3);
        bm[3].gameObject.GetComponent<Image>().sprite = GetnewImage(4);
        bm[4].gameObject.GetComponent<Image>().sprite = GetnewImage(5);
        bm[5].gameObject.GetComponent<Image>().sprite = GetnewImage(6);
    } //새 이미지 뽑힌 걸로 각 box에 붙임

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
        box1Image.GetComponent<Button>().interactable = false;
        box2Image.GetComponent<Button>().interactable = false;
        box3Image.GetComponent<Button>().interactable = false;
        box4Image.GetComponent<Button>().interactable = false;
        box5Image.GetComponent<Button>().interactable = false;
        box6Image.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(1); //1초 대기
    }
    IEnumerator WaitAndPrint()
    {
        yield return AvoidFastClick();
        box1Image.GetComponent<Button>().interactable = true; //1초 대기 후에는 클릭할 수 있어야 함
        box2Image.GetComponent<Button>().interactable = true;
        box3Image.GetComponent<Button>().interactable = true;
        box4Image.GetComponent<Button>().interactable = true;
        box5Image.GetComponent<Button>().interactable = true;
        box6Image.GetComponent<Button>().interactable = true;

        if (X.activeSelf == true)
        {
            X.SetActive(false);
        }
        else
        {
            O.SetActive(false);
            nowStage += 1; //맞으면 다음 스테이지로 자동 이동
            int openStage = PlayerPrefs.GetInt("stage1levelReached");
            if (openStage < nowStage) //최대 깬 스테이지보다 전 스테이지면 갱신하면 안 됨, 더 클 때만 갱신
            {
                PlayerPrefs.SetInt("stage1levelReached", nowStage); //현재 스테이지를 깨면 스테이지락 해제
            }
            if (nowStage == data.Count) //만약 마지막 스테이지라면 스테이지 선택 창으로 돌아감
            {
                ButtonManager.StageToMain();
            }
            else this.q(); //그렇지 않다면 다음 스테이지로    
        }
    } //O,X출력 후 1초 대기

    public void textread() //문제를 읽는다
    {
        tts.read(data[nowStage].Item1);
    }

    public void q() //스테이지 시작
    {
        question.text = data[nowStage].Item1; //문제 텍스트 바꾸기
        answer_image = data[nowStage].Item2; //문제 정답 이미지 인덱스 저장
        this.SetAllnewImage(); //전체 이미지를 새로 뽑는다
        //this.textread(); //문제(이렇게 한 이유: 사운드 버튼 때문)
        
        //정답인 이미지를 위에 붙인다
        if (nowStage >= 1 && nowStage <= 13)
        {
            bm[0].gameObject.GetComponent<Image>().sprite = box1[answer_image];
            answer = 1; //1번 박스가 정답이다
        }
        else if (nowStage >= 14 && nowStage <= 21)
        {
            bm[1].gameObject.GetComponent<Image>().sprite = box2[answer_image];
            answer = 2;
        }
        else if (nowStage >= 22 && nowStage <= 27)
        {
            bm[2].gameObject.GetComponent<Image>().sprite = box3[answer_image];
            answer = 3;
        }
        else if (nowStage >= 28 && nowStage <= 33)
        {
            bm[3].gameObject.GetComponent<Image>().sprite = box4[answer_image];
            answer = 4;
        }
        else if (nowStage >= 34 && nowStage <= 43)
        {
            bm[4].gameObject.GetComponent<Image>().sprite = box5[answer_image];
            answer = 5;
        }
        else if (nowStage >= 44 && nowStage <= 49)
        {
            bm[5].gameObject.GetComponent<Image>().sprite = box6[answer_image];
            answer = 6;
        }

        //위치를 전체 섞는다
        this.mix();
    }

    void mix()
    {
        int[] Imageshuffle = { 0, 1, 2, 3, 4, 5 };
        int tmp;
        for (int i = 0; i < Imageshuffle.Length; i++) //배열 숫자를 섞는다
        {
            int rnd = Random.Range(0, Imageshuffle.Length);
            tmp = Imageshuffle[rnd];
            Imageshuffle[rnd] = Imageshuffle[i];
            Imageshuffle[i] = tmp;
        }
        this.answer = Imageshuffle[answer - 1]; //정답 업데이트
        this.setmix(Imageshuffle[0], box1Image); //섞인 숫자 번호의 좌표로 이미지를 옮긴다
        this.setmix(Imageshuffle[1], box2Image);
        this.setmix(Imageshuffle[2], box3Image);
        this.setmix(Imageshuffle[3], box4Image);
        this.setmix(Imageshuffle[4], box5Image);
        this.setmix(Imageshuffle[5], box6Image);

        bm[0].me = Imageshuffle[0];
        bm[1].me = Imageshuffle[1];
        bm[2].me = Imageshuffle[2];
        bm[3].me = Imageshuffle[3];
        bm[4].me = Imageshuffle[4];
        bm[5].me = Imageshuffle[5];
    } //6개 이미지의 위치를 섞는다

    void setmix(int index, GameObject image)
    {
        float x = mixposition[index, 0];
        float y = mixposition[index, 1];
        float z = mixposition[index, 2];
        //image.transform.localPosition = new Vector3(x, y, z); //자식 객체 이동은 localPosition
        RectTransform rt = (RectTransform)image.transform; //UI이동은 recttransform
        rt.anchoredPosition = new Vector3(x, y, z);
    } //6개 이미지의 위치를 섞는다
}