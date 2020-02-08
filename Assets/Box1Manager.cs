using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box1Manager : MonoBehaviour
{
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnClickBox1()
    {
        gameObject.GetComponent<Image>().sprite = gm.GetnewImage(1);
    }
}