using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box2Manager : MonoBehaviour
{
    public QuestionList2 q2;

    void Update()
    {

    }

    public void OnClickBox2()
    {
        GameObject newImage = q2.getQ2();
        newImage.SetActive(true);
    }
}