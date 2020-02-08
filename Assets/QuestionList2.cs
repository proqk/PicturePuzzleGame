using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionList2 : MonoBehaviour
{
    public GameObject q2; //q2: 파이팅 그룹
    Transform[] childs; //q2 images

    // Start is called before the first frame update
    void Start()
    {
        childs = q2.GetComponentsInChildren<Transform>();

        for(int i = 0; i < childs.Length; i++)
        {
            childs[i].gameObject.SetActive(false);
        }
    }

    public GameObject getQ2()
    {
        int randomQuestionIndex = Random.Range(0, childs.Length);
        return childs[randomQuestionIndex].gameObject;
    }
}
