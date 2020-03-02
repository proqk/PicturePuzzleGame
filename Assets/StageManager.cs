using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public int stageNum;
    public GameObject stageNumObject;

    public void OnClickBox(int n)
    {
        stageNum = n;
        SceneManager.LoadScene("Scene1");
        DontDestroyOnLoad(stageNumObject);
    }
}