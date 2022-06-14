using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    // ���� ������ ���� ��ũ��Ʈ
    // AudioMixer�� �ּ�~�ִ� ���� ���� 0~100�� �ƴ϶� -80���� 0�������� ����
    public AudioMixer mixer;

    public Slider bgmSlider;

    public Text sliderText;

    void Start()
    {
        // MusicVolume = AudioMixer�� Exposed Parameters �̸�
        // bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.75f);
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("BGMVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("BGMVolume", sliderValue);
        //PlayerPrefs.Save();
    }

    // ���� �ۼ�Ʈ�� ǥ���ϱ����� �޼ҵ� (���� ������� �����)
    public void BGMVolumePercent()
    {
        sliderText.text = string.Format("{0:p0}", bgmSlider.value);
    }
}
