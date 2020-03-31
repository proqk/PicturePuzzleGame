using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class tts : MonoBehaviour
{
    public AudioSource audioSource;
    string inputText;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void readText(string s)
    {
        inputText = s;
        StartCoroutine(DownloadTheAudio());
    }

    IEnumerator DownloadTheAudio()
    {
        string url = "https://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=32&client=tw-ob&q="+inputText+"&tl=ko-kr";
        UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG);
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log("오디오 에러남");
            Debug.Log(www.error);
        }
        else
        {
            audioSource.clip = DownloadHandlerAudioClip.GetContent(www);
            audioSource.Play();
        }
    }
}
