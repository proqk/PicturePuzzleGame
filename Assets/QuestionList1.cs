using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionList1 : MonoBehaviour
{
    public GameObject q1; //q1: 좋아요, 사랑해 그룹
    Transform[] childs; //q1 images

    // Start is called before the first frame update
    void Start()
    {
        childs = q1.GetComponentsInChildren<Transform>();

        for(int i = 0; i < childs.Length; i++)
        {
            childs[i].gameObject.SetActive(false);
        }
    }

    public GameObject getQ1()
    {
        int randomQuestionIndex = Random.Range(0, childs.Length);
        return childs[randomQuestionIndex].gameObject;
    }
}
