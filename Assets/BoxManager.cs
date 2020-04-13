using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxManager : MonoBehaviour
{
    public GameManager1 gm1;
    public GameManager2 gm2;
    public GameManager2_level1 gm2_1;
    public GameManager3 gm3;
    public int me;

    public void OnClickBox1()
    {
        if (me == gm1.answer)
        {
            gm1.correct();
        }
        else
        {
            gm1.recallStage(gm1.nowStage);
        }
    }

    public void OnClickBox2()
    {
        if (me == gm2.answer)
        {
            gm2.correct();
        }
        else
        {
            gm2.recallStage(gm2.nowStage);
        }
    }

    public void OnClickBox3()
    {
        if (me == gm3.answer)
        {
            gm3.correct();
        }
        else
        {
            gm3.recallStage(gm3.nowStage);
        }
    }

    public void OnClickBox2_1()
    {
        if (me == gm2_1.answer)
        {
            gm2_1.correct();
        }
        else
        {
            gm2_1.recallStage(gm2_1.nowStage);
        }
    }
}