using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;
using System.IO;

public class GameManager4 : MonoBehaviour
{
    public int answer, nowStage; //정답, 현재 스테이지
    int answer_text;
    public GameObject questionImage; //문제 이미지 오브젝트
    public Text boxtext; //단어
    public csvReader csvreader; //csv파일 읽는 스크립트
    public List<Tuple<string, int>> data; //스테이지와 정답 리스트
    public ButtonManager ButtonManager; //마지막 스테이지에 도달하면 뒤로 가기 버튼과 동일
    public tts tts;
    int nowLevel; //현재 레벨
    bool ispause = false;

    void Start()
    {
        data = csvreader.Read("symbolstage"); //스테이지 정보 불러옴

        nowStage = GameObject.Find("LevelSelector").GetComponent<StageButton>().stageNum;
        nowLevel = GameObject.Find("whatlevel").GetComponent<StageButton>().level;

        if (nowLevel == 1)
        {
            GameObject.Find("ForwardButton").SetActive(false); //앞으로 가는 버튼은 비활성화
            questionImage.GetComponent<Button>().interactable = false; //이미지 버튼 비활성화
            q1();
        }
        else if (nowLevel == 2)
        {
            GameObject.Find("ForwardButton").SetActive(true);
            questionImage.GetComponent<Button>().interactable = true; //이미지 누르면 텍스트 나옴
            GameObject.Find("PauseButton").SetActive(false); //일시정지 버튼 비활성화
            q2();
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(2); //1초 대기
    }

    IEnumerator WaitAndPrint()
    {
        yield return wait();

        nowStage += 1; //맞으면 다음 스테이지로 자동 이동

        if (nowStage == data.Count) //만약 마지막 스테이지라면 스테이지 선택 창으로 돌아감
        {
            ButtonManager.Scene4ToStage4();
        }
        else
        {
            this.q1(); //그렇지 않다면 다음 스테이지로 
        }
    }

    public void textread() //문제를 읽는다
    {
        tts.readText(data[nowStage].Item1);
    }

    public void q1() //자동으로 넘어가기
    {

        PlayerPrefs.SetInt("stage4level1lastStage", nowStage); //마지막 깬 스테이지 저장

        Sprite newImage = Resources.Load<Sprite>(Path.Combine("symbol_Images/", data[nowStage].Item2.ToString()));
        questionImage.gameObject.GetComponent<Image>().sprite = newImage; //이미지 바꿈
        boxtext.text = data[nowStage].Item1; //어휘 바꿈

        this.textread();
        StartCoroutine(WaitAndPrint()); //1초 대기 후 다음
    }

    public void q2() //하나씩 넘어가기
    {
        PlayerPrefs.SetInt("stage4level2lastStage", nowStage);

        Sprite newImage = Resources.Load<Sprite>(Path.Combine("symbol_Images/", data[nowStage].Item2.ToString()));
        questionImage.gameObject.GetComponent<Image>().sprite = newImage; //이미지 바꿈
        boxtext.text = data[nowStage].Item1; //어휘 바꿈

        boxtext.gameObject.SetActive(false); //텍스트는 잠깐 안 보이게
    }
}