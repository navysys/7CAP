using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    // 볼륨 조절을 위한 스크립트
    public AudioSource bgm;
    public Slider bgmSlider;
    public Text sliderText;

    float bgmVol = 1f;

    void Start()
    {
        bgmVol =  PlayerPrefs.GetFloat("BGMVolume", 1f);
        bgmSlider.value = bgmVol;
        bgm.volume = bgmSlider.value;
    }

    void Update()
    {
        SetSoundVolume();

        BGMVolumePercent();
    }

    public void SetSoundVolume()
    {
        bgm.volume = bgmSlider.value;

        bgmVol = bgmSlider.value;
        PlayerPrefs.SetFloat("BGMVolume", bgmVol);
        //PlayerPrefs.Save();
    }

    // 볼륨 퍼센트를 표시하기위한 메소드 (현재 배경음만 적용됨)
    public void BGMVolumePercent()
    {
        sliderText.text = string.Format("{0:p0}", bgmVol);
    }
}
