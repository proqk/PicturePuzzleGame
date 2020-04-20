using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//버튼이 눌리면 스테이지 넘버를 가지고 다음 씬으로 넘어감
public class StageButton : MonoBehaviour
{
    public int stageNum, level; //방금 누른 스테이지 번호, 현재 레벨, 방금 깬 스테이지 번호

    public void setStageNum(int n)
    {
        stageNum = n;
    }

    public void Stage1ToScene1()
    {
        GameObject stageNumObject = GameObject.Find("LevelSelector");
        stageNumObject.GetComponent<StageButton>().stageNum = this.stageNum;
        SceneManager.LoadScene("Scene1");
        DontDestroyOnLoad(stageNumObject);
    }

    public void Stage2ToScene2()
    {
        GameObject stageNumObject = GameObject.Find("LevelSelector"); //지금 누른 스테이지 번호를 보낸다
        stageNumObject.GetComponent<StageButton>().stageNum = this.stageNum;
        DontDestroyOnLoad(stageNumObject);

        SceneManager.LoadScene("Scene2");

    }

    public void Stage3ToScene3()
    {
        GameObject stageNumObject = GameObject.Find("LevelSelector");
        stageNumObject.GetComponent<StageButton>().stageNum = this.stageNum;
        SceneManager.LoadScene("Scene3");
        DontDestroyOnLoad(stageNumObject);
    }
    public void Stage4ToScene4()
    {
        GameObject stageNumObject = GameObject.Find("LevelSelector");
        stageNumObject.GetComponent<StageButton>().stageNum = this.stageNum;
        SceneManager.LoadScene("Scene4");
        DontDestroyOnLoad(stageNumObject);

    }

    public void level1toStage1(int n)
    {
        GameObject stageLevelObject = GameObject.Find("whatlevel");
        stageLevelObject.GetComponent<StageButton>().level = n;
        SceneManager.LoadScene("FirstStage");
        DontDestroyOnLoad(stageLevelObject);

    }

    public void level2toStage2(int n)
    {
        GameObject stageLevelObject = GameObject.Find("whatlevel");
        stageLevelObject.GetComponent<StageButton>().level = n;
        SceneManager.LoadScene("SecondStage");
        DontDestroyOnLoad(stageLevelObject);
    }

    public void level3toStage3(int n)
    {
        GameObject stageLevelObject = GameObject.Find("whatlevel");
        stageLevelObject.GetComponent<StageButton>().level = n;
        SceneManager.LoadScene("ThirdStage");
        DontDestroyOnLoad(stageLevelObject);

    }
    public void leveltoStage4(int n)
    {
        GameObject stageLevelObject = GameObject.Find("whatlevel");
        stageLevelObject.GetComponent<StageButton>().level = n;
        SceneManager.LoadScene("Stage4");
        DontDestroyOnLoad(stageLevelObject);

    }
}