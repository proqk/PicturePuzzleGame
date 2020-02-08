using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 스테이지가 시작할 때 각각 박스의 이미지 6개를 가져온다
 * 정답인 이미지 인덱스를 저장한다
 * 오답이면 정답 빼고 5개 이미지를 1. 새로 뽑아서 2. 위치를 바꾼다
 */
public class GameManager : MonoBehaviour
{
    Sprite[] box1, box2, box3, box4, box5, box6;

    void Start()
    {
        box1 = Resources.LoadAll<Sprite>("1_LikeLove");
        box2 = Resources.LoadAll<Sprite>("2_Fighting");
        box3 = Resources.LoadAll<Sprite>("3_FunHi");
        box4 = Resources.LoadAll<Sprite>("4_SadNice");
        box5 = Resources.LoadAll<Sprite>("5_Sorry");
        box6 = Resources.LoadAll<Sprite>("6_Active");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Sprite GetnewImage(int num)
    {
        int randomQuestionIndex;
        if (num == 1)
        {
            randomQuestionIndex = Random.Range(0, box1.Length);
            return box1[randomQuestionIndex];
        }
        else if (num == 2)
        {
            randomQuestionIndex = Random.Range(0, box2.Length);
            return box2[randomQuestionIndex];
        }
        else if (num == 3)
        {
            randomQuestionIndex = Random.Range(0, box3.Length);
            return box3[randomQuestionIndex];
        }
        else if (num == 4)
        {
            randomQuestionIndex = Random.Range(0, box4.Length);
            return box4[randomQuestionIndex];
        }
        else if (num == 5)
        {
            randomQuestionIndex = Random.Range(0, box5.Length);
            return box5[randomQuestionIndex];
        }
        else if (num == 6)
        {
            randomQuestionIndex = Random.Range(0, box6.Length);
            return box6[randomQuestionIndex];
        }
        return box1[0];
    }
}
