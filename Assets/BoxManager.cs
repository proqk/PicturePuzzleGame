using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxManager : MonoBehaviour
{
    public GameManager gm;
    public int me;

    public void OnClickBox()
    {
        if (me == gm.answer)
        {
            gm.correct();
        }
        else
        {
            gm.recallStage(gm.nowStage);
        }
    }
}