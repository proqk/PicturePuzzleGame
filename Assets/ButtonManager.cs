﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    GameObject sn, BackgroundMusic, gm;
    AudioSource backmusic;

    public void Scene1ToStage1() //감정표현->감정표현 스테이지 화면으로
    {
        sn = GameObject.Find("LevelSelector");
        Destroy(sn);
        SceneManager.LoadScene("FirstStage");
    }
    public void Scene2ToStage2() //상징->상징 스테이지 화면으로
    {
        sn = GameObject.Find("LevelSelector"); //눌렀던 스테이지 번호 정보를 가진 오브젝트 삭제
        Destroy(sn);
        SceneManager.LoadScene("SecondStage");
    }
    public void Scene3ToStage3() //단어->단어표현 스테이지 화면으로
    {
        sn = GameObject.Find("LevelSelector");
        Destroy(sn);
        SceneManager.LoadScene("ThirdStage");
    }

    public void Scene4ToStage4() //학습->어휘학습 표현 스테이지 화면으로
    {
        sn = GameObject.Find("LevelSelector");
        Destroy(sn);
        SceneManager.LoadScene("Stage4");
    }

    public void quitbutton()
    {
        Application.Quit();
    }

    public void Maintolevel1() //첫 화면에서 레벨 선택1 화면으로
    {        
        SceneManager.LoadScene("SelectLevel1");
    }
    public void Maintolevel2() //첫 화면에서 레벨 선택2 화면으로
    {
        SceneManager.LoadScene("SelectLevel2");
    }
    public void Maintolevel3() //첫 화면에서 레벨 선택3 화면으로
    {
        SceneManager.LoadScene("SelectLevel3");
    }
    public void Maintolevel4() //첫 화면에서 레벨 선택4 화면으로
    {
        SceneManager.LoadScene("SelectLevel4");
    }

    public void Stage1tolevel1() //스테이지1 화면에서 레벨 선택1 화면으로
    {
        sn = GameObject.Find("whatlevel");
        Destroy(sn);
        SceneManager.LoadScene("SelectLevel1");
    }
    public void Stage2tolevel2() //스테이지2 화면에서 레벨 선택2 화면으로
    {
        sn = GameObject.Find("whatlevel");
        Destroy(sn);
        SceneManager.LoadScene("SelectLevel2");
    }
    public void Stage3tolevel3() //스테이지3 화면에서 레벨 선택3 화면으로
    {
        sn = GameObject.Find("whatlevel");
        Destroy(sn);
        SceneManager.LoadScene("SelectLevel3");
    }
    public void Stage4tolevel4() //스테이지3 화면에서 레벨 선택3 화면으로
    {
        sn = GameObject.Find("whatlevel");
        Destroy(sn);
        SceneManager.LoadScene("SelectLevel4");
    }

    public void MaintoScene5() //첫 화면에서 설정/앱 정보 화면으로
    {
        SceneManager.LoadScene("appInfo");
    }

    public void StageToMain() //스테이지에서 메인으로
    {
        SceneManager.LoadScene("Main");
    }

    public void MainToStart() //메인에서 첫 화면으로
    {
        SceneManager.LoadScene("Start");
    }

    public void SoundButtonScene1() //문제 텍스트 읽는 소리 버튼
    {
        gm = GameObject.Find("GameManager");
        gm.GetComponent<GameManager1>().textread();
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
    public void SoundButtonScene2() //문제 텍스트 읽는 소리 버튼
    {
        gm = GameObject.Find("GameManager");
        gm.GetComponent<GameManager2>().textread();

    }

    public void SoundButtonScene3() //문제 텍스트 읽는 소리 버튼
    {
        gm = GameObject.Find("GameManager");
        gm.GetComponent<GameManager3>().textread();

    }
    public void SoundButtonScene4() //문제 텍스트 읽는 소리 버튼
    {
        gm = GameObject.Find("GameManager");
        gm.GetComponent<GameManager4>().textread();

    }

    public void BackGroundMusicOffButton() //배경음악 키고 끄는 버튼
    {
        BackgroundMusic = GameObject.Find("BackgroundMusic");
        backmusic = BackgroundMusic.GetComponent<AudioSource>(); //배경음악 저장해둠
        if (backmusic.isPlaying) backmusic.Pause();
        else backmusic.Play();
    }

    public void ScrollToTop()
    {
        GameObject.Find("ScrollRect").GetComponent<ScrollRect>().verticalNormalizedPosition = 1f;
    }

    public void ScrollToBottom()
    {
        GameObject.Find("ScrollRect").GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
    }

    bool ispause = false;
    public Sprite pause, play;
    public void PauseButton() //일시정지 버튼
    {
        ispause = !ispause;
        if (ispause)
        {
            Time.timeScale = 0;
            this.gameObject.GetComponent<Image>().sprite = pause;
        }
        else
        {
            Time.timeScale = 1;
            this.gameObject.GetComponent<Image>().sprite = play;
        }
    }
    //void OnApplicationPause(bool pauseStatus) //어플을 내렸을 때 자동으로 멈추게 하기
    //{
    //    ispause = true;
    //    Time.timeScale = 0;
    //}

    public void ForwardButton()
    {
        GameManager4 gm = GameObject.Find("GameManager").GetComponent<GameManager4>();
        gm.nowStage += 1;

        if (gm.nowStage == gm.data.Count) //만약 마지막 스테이지라면 스테이지 선택 창으로 돌아감
        {
            Scene4ToStage4();
        }
        else
        {
            gm.q2(); //그렇지 않다면 다음 스테이지로 
        }
    }

    public void Stage4ImageBlick() //이미지가 눌리면 텍스트를 보인다
    {
        GameManager4 gm = GameObject.Find("GameManager").GetComponent<GameManager4>();
        gm.boxtext.gameObject.SetActive(true);
        gm.textread();
    }
}