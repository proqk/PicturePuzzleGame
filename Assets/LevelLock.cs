using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelLock : MonoBehaviour
{
    int levelat; //현재 스테이지 번호, 오픈한 스테이지 번호
    public GameObject stageNumObject;
    bool isfirstplay = true;

    void Start()
    {
        //_ = stageNumObject.GetComponentInChildren<GameObject>(); //이건 왜 되지?
        Button[] stages = stageNumObject.GetComponentsInChildren<Button>();
        string nowScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        int nowLevel = GameObject.Find("whatlevel").GetComponent<StageButton>().level;

        if (PlayerPrefs.GetInt("stage1level1Reached") == 0) PlayerPrefs.SetInt("stage1level1Reached", 0);
        if (PlayerPrefs.GetInt("stage2level1Reached") == 0) PlayerPrefs.SetInt("stage2level1Reached", 1);
        if (PlayerPrefs.GetInt("stage3level1Reached") == 0) PlayerPrefs.SetInt("stage3level1Reached", 1);
        if (PlayerPrefs.GetInt("stage1level2Reached") == 0) PlayerPrefs.SetInt("stage1level2Reached", 0);
        if (PlayerPrefs.GetInt("stage2level2Reached") == 0) PlayerPrefs.SetInt("stage2level2Reached", 1);
        if (PlayerPrefs.GetInt("stage3level2Reached") == 0) PlayerPrefs.SetInt("stage3level2Reached", 1);



        if (nowScene == "FirstStage" && nowLevel == 1) //스테이지1이면서 레벨1
        {
            levelat = PlayerPrefs.GetInt("stage1level1Reached") + 1;
        }
        else if (nowScene == "FirstStage" && nowLevel == 2) //스테이지1이면서 레벨2
        {
            levelat = PlayerPrefs.GetInt("stage1level2Reached") + 1;
        }
        else if (nowScene == "SecondStage" && nowLevel == 1) //스테이지2면서 레벨1
        {
            levelat = PlayerPrefs.GetInt("stage2level1Reached");
        }
        else if (nowScene == "SecondStage" && nowLevel == 2) //스테이지2면서 레벨2
        {
            levelat = PlayerPrefs.GetInt("stage2level2Reached");
        }
        else if (nowScene == "ThirdStage" && nowLevel == 1) //스테이지3이면서 레벨1
        {
            levelat = PlayerPrefs.GetInt("stage3level1Reached");
        }
        else if (nowScene == "ThirdStage" && nowLevel == 2) //스테이지3이면서 레벨2
        {
            levelat = PlayerPrefs.GetInt("stage3level2Reached");
        }

        for (int i = levelat; i < stages.Length; i++)
        {
            stages[i].interactable = false;
        }
    }
}