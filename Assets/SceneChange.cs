using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int stageNum;
    public GameObject stageNumObject;

    // Start is called before the first frame update
    public void call()
    {
        SceneManager.LoadScene("Scene1");
        DontDestroyOnLoad(stageNumObject);
    }
}
