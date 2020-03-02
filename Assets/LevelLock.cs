using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLock : MonoBehaviour
{
    int levelat; //현재 스테이지 번호, 오픈한 스테이지 번호
    public GameObject stageNumObject;

    void Start()
    {
        //_ = stageNumObject.GetComponentInChildren<GameObject>(); //이건 왜 되지?
        Button[] stages = stageNumObject.GetComponentsInChildren<Button>();

        levelat = PlayerPrefs.GetInt("levelReached");
        print(levelat);
        for (int i = levelat + 1; i < stages.Length; i++)
        {
            stages[i].interactable = false;
        }
    }
}