using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // �̱����� �Ҵ��� ���� ����

    AudioSource playerAudio; // SoundManager ������Ʈ (�÷��̾� ĳ������ ���� ���)
    AudioSource deathSound; // DeathSound ������Ʈ (SoundManager�� �ڽ� ������Ʈ)
    AudioSource[] BGMList; // BGM ������Ʈ

    //float start = 0.5f;
    //float end = 0f;
   // float fadeTime = 2f; // ���̵� ȿ�� ����ð�

    public AudioClip[] BGM = new AudioClip[3]; // BGM �ӽ� ����
    public AudioClip tmpClip; // �ӽ� �����Ŭ��

    // �̱���
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
            Debug.LogError("Scene�� �� �� �̻��� SoundManager�� �����մϴ�!");
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
    */

    // �����(BGM) ���
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

    // �����(BGM) �Ͻ�����
    public void AudioPause()
    {
        playerAudio.Pause();

        for (int i = 0; i < 3; i++)
            BGMList[i].Pause();
    }

    // �����(BGM) ����
    public void AudioStop()
    {
        playerAudio.Stop();

        for (int i = 0; i < 3; i++)
            BGMList[i].Stop();
    }

    // ����
    public void PlayJumpSound()
    {
        playerAudio.PlayOneShot(tmpClip);
    }
}
