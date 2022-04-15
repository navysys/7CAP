using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Scene 전환을 위한 스크립트
public class SceneDirector : MonoBehaviour
{
    public static SceneDirector instance;

    public GameObject fadeImg;

    public float fadeTime = 2f; // 페이드 효과 재생시간
    public bool isPlaying = false; // 페이드 효과가 진행 중임을 체크하는 변수

    float start = 0f;
    float end = 1f;
    float time = 0f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {

    }

    // 타이틀 씬 전환
    public void TitleSceneChange()
    {
        SceneManager.LoadScene("Title");
    }
    
    // 게임 맵 씬 전환
    public void GameMapSceneChange()
    {
        ChangeBg();
    }

    // 전투 씬 전환
    public void BattleSceneChange()
    {
        StartCoroutine(WaitForBattleScene());
    }

    // 게임 플레이 방법 씬 전환
    public void HowtoPlayChange()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    // 게임 오버 씬
    public static void GameOverChange()
    {
        SceneManager.LoadScene("GameOver");
    }

    // 다시 시작
    public void RestartChange()
    {
        SceneManager.LoadScene("Title");
    }

    public void ChangeBg()
    {
        // 페이드 아웃
        fadeImg.SetActive(true);
        StartCoroutine(FadeOutBackground());
    }

    void OnEnable()
    {
        // 델리게이트 체인 추가
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("씬 교체됨, 현재 씬: " + scene.name);

        // 씬 전환 효과 (페이드 인)
        //StartCoroutine(FadeInBackground());
    }

    void OnDisable()
    {
        // 델리게이트 체인 제거
        //SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 씬 전환을 위한 코루틴 (페이드 아웃)
    IEnumerator FadeOutBackground()
    {
        isPlaying = true;

        Color bgColor = fadeImg.GetComponent<Image>().color;
        time = 0f;
        bgColor.a = Mathf.Lerp(start, end, time);

        // 이미지 색상의 알파값이 255으로 가까워 질 수록 불투명해짐
        while (bgColor.a < 1f)
        {
            time += Time.deltaTime / fadeTime; // 지정한 시간 (FadeTime) 만큼 페이드 인 효과를 주기 위해 1초를 나눔
            bgColor.a = Mathf.Lerp(start, end, time); // start와 end의 중간값을 리턴
            fadeImg.GetComponent<Image>().color = bgColor;

            yield return null; // 다음 프레임까지 대기
        }

        isPlaying = false;

        SceneManager.LoadScene("GameMap");
    }

    // 씬 전환을 위한 코루틴 (페이드 인)
    IEnumerator FadeInBackground()
    {
        isPlaying = true;

        Color bgColor = fadeImg.GetComponent<Image>().color;
        start = 1f;
        end = 0f;
        time = 0f;
        bgColor.a = Mathf.Lerp(start, end, time);

        // 이미지 색상의 알파값이 0으로 가까워 질 수록 투명해짐
        while (bgColor.a > 0f)
        {
            time += Time.deltaTime / fadeTime; // 지정한 시간 (FadeTime) 만큼 페이드 인 효과를 주기 위해 1초를 나눔
            bgColor.a = Mathf.Lerp(start, end, time); // start와 end의 중간값을 리턴
            fadeImg.GetComponent<Image>().color = bgColor;

            yield return null; // 다음 프레임까지 대기
        }

        isPlaying = false;
    }

    // n초 뒤에 전투 씬으로 넘어감
    IEnumerator WaitForBattleScene()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Battle");
    }
}
