﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    //스테이지에다가 버튼을 생성함-총 스테이지 개수만큼 자동 생성
    public GameObject button;
    public string filename;
    public Sprite lastbutton;

    void Awake()
    {
        //if (PlayerPrefs.GetInt("stage1level1lastStage") == 0) PlayerPrefs.SetInt("stage1level1lastStage", 1);
        //if (PlayerPrefs.GetInt("stage2level1lastStage") == 0) PlayerPrefs.SetInt("stage2level1lastStage", 1);
        //if (PlayerPrefs.GetInt("stage3level1lastStage") == 0) PlayerPrefs.SetInt("stage3level1lastStage", 1);
        //if (PlayerPrefs.GetInt("stage1level2lastStage") == 0) PlayerPrefs.SetInt("stage1level2lastStage", 1);
        //if (PlayerPrefs.GetInt("stage2level2lastStage") == 0) PlayerPrefs.SetInt("stage2level2lastStage", 1);
        //if (PlayerPrefs.GetInt("stage3level2lastStage") == 0) PlayerPrefs.SetInt("stage3level2lastStage", 1);

//        bool isSceneToStage = true; //씬에서 스테이지로 넘어왔으면 마지막 풀었던 문제 버튼을 색깔 바꿔줘야 함
        int nowLevel = GameObject.Find("whatlevel").GetComponent<StageButton>().level;

        TextAsset sourcefile = Resources.Load<TextAsset>(filename);
        StringReader sr = new StringReader(sourcefile.text);
        GameObject content = GameObject.Find("Content");
        int len = sourcefile.text.Split(',').Length - 1; //왜 하나 더 나옴? 49개면 왜 50개 나옴?

        string nowStage = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        int laststage = 0;

        if (nowStage == "FirstStage" && nowLevel == 1) laststage = PlayerPrefs.GetInt("stage1level1lastStage");
        if (nowStage == "FirstStage" && nowLevel == 2) laststage = PlayerPrefs.GetInt("stage1level2lastStage");
        if (nowStage == "SecondStage" && nowLevel == 1) laststage = PlayerPrefs.GetInt("stage2level1lastStage");
        if (nowStage == "SecondStage" && nowLevel == 2) laststage = PlayerPrefs.GetInt("stage2level2lastStage");
        if (nowStage == "ThirdStage" && nowLevel == 1) laststage = PlayerPrefs.GetInt("stage3level1lastStage");
        if (nowStage == "ThirdStage" && nowLevel == 2) laststage = PlayerPrefs.GetInt("stage3level2lastStage");

        int stagenum = 1; //각 버튼에 스테이지 번호 매김(len은 0~len-1, 숫자는 1~len)
        //1스테는 0~len-1, 나머지는 1~len

        if (nowStage == "FirstStage")
        {
            for (int i = 0; i < len; i++)
            {
                GameObject newbutton = Instantiate(button);
                newbutton.transform.SetParent(content.transform);
                newbutton.GetComponentInChildren<Text>().text = stagenum.ToString(); //겉에 보여지는 숫자는 1부터
                newbutton.GetComponent<StageButton>().setStageNum(i); //실제 넘어가는 숫자는 1스테는 0부터 나머지는 0부터
                stagenum += 1;

                if (i == laststage) newbutton.GetComponent<Image>().sprite = lastbutton;
            }
        }
        else
        {
            for (int i = 1; i < len; i++)
            {
                GameObject newbutton = Instantiate(button);
                newbutton.transform.SetParent(content.transform);
                newbutton.GetComponentInChildren<Text>().text = i.ToString(); //겉에 보여지는 숫자는 1부터
                newbutton.GetComponent<StageButton>().setStageNum(i);

                if(i == laststage) newbutton.GetComponent<Image>().sprite = lastbutton;
                //마지막으로 했던 스테이지면 색깔 바꿔줌
            }
        }

    }

}