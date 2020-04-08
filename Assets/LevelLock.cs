using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelLock : MonoBehaviour
{
    int levelat; //현재 스테이지 번호, 오픈한 스테이지 번호
    public GameObject stageNumObject;

    void Start()
    {
        //_ = stageNumObject.GetComponentInChildren<GameObject>(); //이건 왜 되지?
        Button[] stages = stageNumObject.GetComponentsInChildren<Button>();
        string nowScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (PlayerPrefs.GetInt("stage1levelReached") == 0) PlayerPrefs.SetInt("stage1levelReached", 1);
        if (PlayerPrefs.GetInt("stage2levelReached") == 0) PlayerPrefs.SetInt("stage2levelReached", 1);
        if (PlayerPrefs.GetInt("stage3levelReached") == 0) PlayerPrefs.SetInt("stage3levelReached", 1);
        //if (PlayerPrefs.GetInt("stage2levelReached") == 0) PlayerPrefs.SetInt("stage2levelReached", 200);
        //PlayerPrefs.SetInt("stage3levelReached", 400);

        if (nowScene == "FirstStage")
        {
            levelat = PlayerPrefs.GetInt("stage1levelReached");
        }
        else if (nowScene == "SecondStage")
        {
            levelat = PlayerPrefs.GetInt("stage2levelReached");
        }
        else if (nowScene == "ThirdStage")
        {
            levelat = PlayerPrefs.GetInt("stage3levelReached");
        }

        for (int i = levelat; i < stages.Length; i++)
        {
            stages[i].interactable = false;
        }
    }
}