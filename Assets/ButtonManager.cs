using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    GameObject sn;

    public void onClick()
    {
        sn = GameObject.Find("LevelSelector");
        Destroy(sn);
        SceneManager.LoadScene("Stage");
    }
}