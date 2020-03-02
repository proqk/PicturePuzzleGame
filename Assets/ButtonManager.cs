using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    GameObject sn, BackgroundMusic, gm;
    AudioSource backmusic;

    public void onClick() //문제 화면->스테이지 화면으로 돌아가기
    {
        sn = GameObject.Find("LevelSelector");
        Destroy(sn);
        SceneManager.LoadScene("Stage");
    }

    public void StartSceneOnClick() //첫 화면에서 스테이지 화면으로
    {        
        SceneManager.LoadScene("Stage");
    }

    public void StageSceneOnClick() //스테이지에서 첫화면으로
    {
        SceneManager.LoadScene("FirstScene");
    }

    public void SoundButton() //문제 텍스트 읽는 소리 버튼
    {
        gm = GameObject.Find("GameManager");
        gm.GetComponent<GameManager>().textread();
        /*
         여기서
         tts = GameObject.Find("tts");
         tts.GetComponet<TexttoSpeach>().read(text); 해서
         바로 tts에 접근, 소리 재생하고 싶은데 
         text값을 함수의 매개변수로 받게 되면 gm의 q()는 한 번 실행 되고 사운드 버튼을 2번 이상 누르는 경우
         text값을 찾을 수 없어져서 안 됨
         왜냐하면 soundbutton은 누를 때마다 새로 호출되는데 q()는 스테이지 시작에 한 번 시작이니까 
         좀 더 깔끔하게 짤 수 있는 방법을 모르겠음(gm에 함수를 추가하고 싶지 않았음)      
         */
    }

    public void BackGroundMusicOffButton() //배경음악 키고 끄는 버튼
    {
        BackgroundMusic = GameObject.Find("BackgroundMusic");
        backmusic = BackgroundMusic.GetComponent<AudioSource>(); //배경음악 저장해둠
        if (backmusic.isPlaying) backmusic.Pause();
        else backmusic.Play();
    }
}