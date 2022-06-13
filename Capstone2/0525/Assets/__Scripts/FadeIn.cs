using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public GameObject fadeImg;

    public float fadeTime = 2f; // ���̵� ȿ�� ����ð�
    public bool isPlaying = false; // ���̵� ȿ���� ���� ������ üũ�ϴ� ����

    float start = 1f;
    float end = 0f;
    float time = 0f;

    // �� ��ȯ�� ���� �ڷ�ƾ (���̵� ��)
    IEnumerator FadeInBackground()
    {
        isPlaying = true;

        Color bgColor = fadeImg.GetComponent<Image>().color;
        time = 0f;
        bgColor.a = Mathf.Lerp(start, end, time);

        // �̹��� ������ ���İ��� 0���� ����� �� ���� ��������
        while (bgColor.a > 0f)
        {
            time += Time.deltaTime / fadeTime; // ������ �ð� (FadeTime) ��ŭ ���̵� �� ȿ���� �ֱ� ���� 1�ʸ� ����
            bgColor.a = Mathf.Lerp(start, end, time); // start�� end�� �߰����� ����
            fadeImg.GetComponent<Image>().color = bgColor;

            yield return null; // ���� �����ӱ��� ���
        }

        isPlaying = false;
    }
}
