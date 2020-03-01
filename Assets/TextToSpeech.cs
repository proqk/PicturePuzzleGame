using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TextToSpeech : MonoBehaviour
{
    AudioSource audioSource;
    public void read(string text)
    {
        audioSource = gameObject.GetComponent<AudioSource>();

        //텍스트에 ?나 !가 있다면 없애줌(오디오 파일명 일치)
        if (text.IndexOf("!") != -1 || text.IndexOf("?") != -1)
        {
            text = System.Text.RegularExpressions.Regex.Replace(text, @"[!?]", "");
        }

        audioSource.clip = Resources.Load<AudioClip>("Audios/"+text);
        audioSource.Play();
    }
}