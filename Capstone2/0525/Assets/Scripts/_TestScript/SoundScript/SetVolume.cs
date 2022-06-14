using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    // ���� ������ ���� ��ũ��Ʈ
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

    // ���� �ۼ�Ʈ�� ǥ���ϱ����� �޼ҵ� (���� ������� �����)
    public void BGMVolumePercent()
    {
        sliderText.text = string.Format("{0:p0}", bgmVol);
    }
}
