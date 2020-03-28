using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//버튼이 눌리면 스테이지 넘버를 가지고 다음 씬으로 넘어감
public class StageButton : MonoBehaviour
{
    public int stageNum;
    public Sprite[] box;

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
        GameObject images = GameObject.Find("symbolImages");

        stageNumObject.GetComponent<StageButton>().stageNum = this.stageNum;
        SceneManager.LoadScene("Scene2");
        DontDestroyOnLoad(stageNumObject);

        if (images.GetComponent<StageButton>().box.Length == 0)
        {
            Sprite[] imageBox1 = Resources.LoadAll<Sprite>("symbol_Image_1");
            Sprite[] imageBox2 = Resources.LoadAll<Sprite>("symbol_Image_2");
//            Sprite[] imageBox3 = Resources.LoadAll<Sprite>("symbol_Image_3");
//            Sprite[] imageBox4 = Resources.LoadAll<Sprite>("symbol_Image_4");
//            Sprite[] imageBox5 = Resources.LoadAll<Sprite>("symbol_Image_5");

            var list = new List<Sprite>();
            list.AddRange(imageBox1);
            list.AddRange(imageBox2);
//            list.AddRange(imageBox3);
//            list.AddRange(imageBox4);
//            list.AddRange(imageBox5);
            Sprite[] imageBox = list.ToArray();
            Array.Sort(imageBox, delegate (Sprite x, Sprite y) { return int.Parse(x.name).CompareTo(int.Parse(y.name)); });

            images.GetComponent<StageButton>().box = imageBox;
            DontDestroyOnLoad(images);
        }
        else return;
    }

    public void Stage3ToScene3()
    {
        GameObject stageNumObject = GameObject.Find("LevelSelector");
        GameObject images = GameObject.Find("symbolImages");
        stageNumObject.GetComponent<StageButton>().stageNum = this.stageNum;
        SceneManager.LoadScene("Scene3");
        DontDestroyOnLoad(stageNumObject);

        if (images.GetComponent<StageButton>().box.Length == 0)
        {
            Sprite[] imageBox1 = Resources.LoadAll<Sprite>("symbol_Image_1");
            Sprite[] imageBox2 = Resources.LoadAll<Sprite>("symbol_Image_2");
            //            Sprite[] imageBox3 = Resources.LoadAll<Sprite>("symbol_Image_3");
            //            Sprite[] imageBox4 = Resources.LoadAll<Sprite>("symbol_Image_4");
            //            Sprite[] imageBox5 = Resources.LoadAll<Sprite>("symbol_Image_5");

            var list = new List<Sprite>();
            list.AddRange(imageBox1);
            list.AddRange(imageBox2);
            //            list.AddRange(imageBox3);
            //            list.AddRange(imageBox4);
            //            list.AddRange(imageBox5);
            Sprite[] imageBox = list.ToArray();
            Array.Sort(imageBox, delegate (Sprite x, Sprite y) { return int.Parse(x.name).CompareTo(int.Parse(y.name)); });

            images.GetComponent<StageButton>().box = imageBox;
            DontDestroyOnLoad(images);
        }
        else return;
    }
}