using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // 싱글톤을 할당할 전역 변수

    public AudioSource bgm; // 배경음
    public AudioSource effect;  // 효과음

    public Slider bgmSlider;
    public Slider effectSlider;

    public Text bgmSliderText;
    public Text effectSliderText;

    float bgmVol = 1f;
    float effectVol = 1f;

    //float start = 0.5f;
    //float end = 0f;
    // float fadeTime = 2f; // 페이드 효과 재생시간

    // 싱글톤
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            //Debug.LogError("Scene에 두 개 이상의 SoundManager가 존재합니다!");
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        bgmVol = PlayerPrefs.GetFloat("BGMVolume", 1f);
        effectVol = PlayerPrefs.GetFloat("EffectVolume", 1f);

        bgmSlider.value = bgmVol;
        bgm.volume = bgmSlider.value;

        effectSlider.value = effectVol;
        effect.volume = effectSlider.value;
    }

    void Update()
    {
        SetSoundVolume();

        GetVolumePercent();
    }

    // 볼륨 조절을 위한 메소드
    public void SetSoundVolume()
    {
        bgm.volume = bgmSlider.value;

        bgmVol = bgmSlider.value;
        PlayerPrefs.SetFloat("BGMVolume", bgmVol);

        effect.volume = effectSlider.value;

        effectVol = effectSlider.value;
        PlayerPrefs.SetFloat("EffectVolume", effectVol);
        //PlayerPrefs.Save();
    }

    // 볼륨 퍼센트를 표시하기위한 메소드 (현재 배경음만 적용됨)
    public void GetVolumePercent()
    {
        bgmSliderText.text = string.Format("{0:p0}", bgmVol);
        effectSliderText.text = string.Format("{0:p0}", effectVol);
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
    

    // 오디오(BGM) 재생
    public void AudioPlay()
    {
        //bgm.Play();
    }

    // 오디오(BGM) 일시정지
    public void AudioPause()
    {
        //bgm.Pause();
    }

    // 오디오(BGM) 정지
    public void AudioStop()
    {
        //bgm.Stop();
    }

    // 사운드
    public void PlayEffectSound()
    {
        //bgm.PlayOneShot(tmpClip);
    }
    */
}
