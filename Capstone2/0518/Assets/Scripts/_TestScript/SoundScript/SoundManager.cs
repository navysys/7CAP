using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // 싱글톤을 할당할 전역 변수

    AudioSource playerAudio; // SoundManager 오브젝트 (플레이어 캐릭터의 사운드 담당)
    AudioSource deathSound; // DeathSound 오브젝트 (SoundManager의 자식 오브젝트)
    AudioSource[] BGMList; // BGM 오브젝트

    //float start = 0.5f;
    //float end = 0f;
   // float fadeTime = 2f; // 페이드 효과 재생시간

    public AudioClip[] BGM = new AudioClip[3]; // BGM 임시 저장
    public AudioClip tmpClip; // 임시 오디오클립

    // 싱글톤
    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            BGMList = new AudioSource[3];
            for (int i = 0; i < 3; i++)
            {
                GameObject newBGM = new GameObject("Theme BGM " + (i + 1));
                BGMList[i] = newBGM.AddComponent<AudioSource>();
                BGMList[i].clip = BGM[i];
                BGMList[i].volume = 0.5f;
                newBGM.transform.parent = transform;
            }

            for (int i = 1; i < 3; i++)
            {
                BGMList[i].gameObject.SetActive(false);
            }
        }

        else
        {
            Debug.LogError("Scene에 두 개 이상의 SoundManager가 존재합니다!");
            Destroy(gameObject);
        }

    }

    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        deathSound = GameObject.Find("DeathSound").GetComponent<AudioSource>();
    }
    /*
    void Update()
    {
        if (GameManager.instance.isGameOver)
        {
           return;
        }

        StartCoroutine(AudioCrossfade());
    }
    
    // BGM 크로스페이드
    IEnumerator AudioCrossfade()
    {
        
        if (GameManager.instance.stage == 2)
        {
            float percent = 0;

            BGMList[1].gameObject.SetActive(true);

            while (percent < 1)
            {
                percent += Time.deltaTime * 1 / fadeTime;
                BGMList[0].volume = Mathf.Lerp(start, end, percent); // 페이드 아웃
                BGMList[1].volume = Mathf.Lerp(end, start, percent); // 페이드 인
                yield return null;
            }

            BGMList[0].gameObject.SetActive(false);
        }

        if (GameManager.instance.stage == 3)
        {
            float percent = 0;

            BGMList[2].gameObject.SetActive(true);

            while (percent < 1)
            {
                percent += Time.deltaTime * 1 / fadeTime;
                BGMList[1].volume = Mathf.Lerp(start, end, percent); // 페이드 아웃
                BGMList[2].volume = Mathf.Lerp(end, start, percent); // 페이드 인
                yield return null;
            }

            BGMList[1].gameObject.SetActive(false);
        }
        
    }
    */

    // 오디오(BGM) 재생
    public void AudioPlay()
    {
        playerAudio.Play();

        for (int i = 0; i < 3; i++)
        {
            if (BGMList[i].gameObject.activeSelf == true)
            {
                BGMList[i].Play();
            }
        }
    }

    // 오디오(BGM) 일시정지
    public void AudioPause()
    {
        playerAudio.Pause();

        for (int i = 0; i < 3; i++)
            BGMList[i].Pause();
    }

    // 오디오(BGM) 정지
    public void AudioStop()
    {
        playerAudio.Stop();

        for (int i = 0; i < 3; i++)
            BGMList[i].Stop();
    }

    // 사운드
    public void PlayJumpSound()
    {
        playerAudio.PlayOneShot(tmpClip);
    }
}
