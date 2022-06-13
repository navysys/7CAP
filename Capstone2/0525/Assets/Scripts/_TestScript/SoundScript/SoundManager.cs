using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // �̱����� �Ҵ��� ���� ����

    public AudioSource bgm; // �����
    public AudioSource effect;  // ȿ����

    public Slider bgmSlider;
    public Slider effectSlider;

    public Text bgmSliderText;
    public Text effectSliderText;

    float bgmVol = 1f;
    float effectVol = 1f;

    //float start = 0.5f;
    //float end = 0f;
    // float fadeTime = 2f; // ���̵� ȿ�� ����ð�

    // �̱���
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            //Debug.LogError("Scene�� �� �� �̻��� SoundManager�� �����մϴ�!");
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

    // ���� ������ ���� �޼ҵ�
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

    // ���� �ۼ�Ʈ�� ǥ���ϱ����� �޼ҵ� (���� ������� �����)
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
    
    // BGM ũ�ν����̵�
    IEnumerator AudioCrossfade()
    {
        
        if (GameManager.instance.stage == 2)
        {
            float percent = 0;

            BGMList[1].gameObject.SetActive(true);

            while (percent < 1)
            {
                percent += Time.deltaTime * 1 / fadeTime;
                BGMList[0].volume = Mathf.Lerp(start, end, percent); // ���̵� �ƿ�
                BGMList[1].volume = Mathf.Lerp(end, start, percent); // ���̵� ��
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
                BGMList[1].volume = Mathf.Lerp(start, end, percent); // ���̵� �ƿ�
                BGMList[2].volume = Mathf.Lerp(end, start, percent); // ���̵� ��
                yield return null;
            }

            BGMList[1].gameObject.SetActive(false);
        }
        
    }
    

    // �����(BGM) ���
    public void AudioPlay()
    {
        //bgm.Play();
    }

    // �����(BGM) �Ͻ�����
    public void AudioPause()
    {
        //bgm.Pause();
    }

    // �����(BGM) ����
    public void AudioStop()
    {
        //bgm.Stop();
    }

    // ����
    public void PlayEffectSound()
    {
        //bgm.PlayOneShot(tmpClip);
    }
    */
}
