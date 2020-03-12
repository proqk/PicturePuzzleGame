using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//버튼이 눌리면 스테이지 넘버를 가지고 다음 씬으로 넘어감
public class StageButton : MonoBehaviour
{
    public int stageNum;

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
        GameObject stageNumObject = GameObject.Find("LevelSelector");
        stageNumObject.GetComponent<StageButton>().stageNum = this.stageNum;
        SceneManager.LoadScene("Scene2");
        DontDestroyOnLoad(stageNumObject);
    }

    public void Stage3ToScene3()
    {
        GameObject stageNumObject = GameObject.Find("LevelSelector");
        stageNumObject.GetComponent<StageButton>().stageNum = this.stageNum;
        SceneManager.LoadScene("Scene3");
        DontDestroyOnLoad(stageNumObject);
    }
}