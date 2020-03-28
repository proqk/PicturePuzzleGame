using System;
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

    void Awake()
    {
        TextAsset sourcefile = Resources.Load<TextAsset>(filename);
        StringReader sr = new StringReader(sourcefile.text);
        GameObject content = GameObject.Find("Content");
        int len = sourcefile.text.Split(',').Length;
        //일단 200개만
        for (int i = 1; i <= 200; i++)
        {
            GameObject newbutton = Instantiate(button);
            newbutton.transform.SetParent(content.transform);
            newbutton.GetComponentInChildren<Text>().text = i.ToString();
            newbutton.GetComponent<StageButton>().setStageNum(i);
        }
    }

}