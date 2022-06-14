using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    // 볼륨 조절을 위한 스크립트
    // AudioMixer의 최소~최대 볼륨 값이 0~100이 아니라 -80에서 0까지임을 유의
    public AudioMixer mixer;

    public Slider bgmSlider;

    public Text sliderText;

    void Start()
    {
        // MusicVolume = AudioMixer의 Exposed Parameters 이름
        // bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.75f);
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("BGMVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("BGMVolume", sliderValue);
        //PlayerPrefs.Save();
    }

    // 볼륨 퍼센트를 표시하기위한 메소드 (현재 배경음만 적용됨)
    public void BGMVolumePercent()
    {
        sliderText.text = string.Format("{0:p0}", bgmSlider.value);
    }
}
