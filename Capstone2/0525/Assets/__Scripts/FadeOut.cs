using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public GameObject fadeImg;

    public float fadeTime = 2f; // ���̵� ȿ�� ����ð�
    public bool isPlaying = false; // ���̵� ȿ���� ���� ������ üũ�ϴ� ����

    float start = 0f;
    float end = 1f;
    float time = 0f;

    // �� ��ȯ�� ���� �ڷ�ƾ (���̵� �ƿ�)
    IEnumerator FadeOutBackground()
    {
        isPlaying = true;

        Color bgColor = fadeImg.GetComponent<Image>().color;
        time = 0f;
        bgColor.a = Mathf.Lerp(start, end, time);

        // �̹��� ������ ���İ��� 255���� ����� �� ���� ����������
        while (bgColor.a < 1f)
        {
            time += Time.deltaTime / fadeTime; // ������ �ð� (FadeTime) ��ŭ ���̵� �� ȿ���� �ֱ� ���� 1�ʸ� ����
            bgColor.a = Mathf.Lerp(start, end, time); // start�� end�� �߰����� ����
            fadeImg.GetComponent<Image>().color = bgColor;

            yield return null; // ���� �����ӱ��� ���
        }

        isPlaying = false;
    }
}
