using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    GameObject player;

    public GameObject Canvas;

    [SerializeField] GameObject fadeImg;
    [SerializeField] GameObject upStateBar;
    [SerializeField] TextMeshProUGUI barCharacterName;
    [SerializeField] TextMeshProUGUI barHP;
    [SerializeField] TextMeshProUGUI barGold;
    public TextMeshProUGUI DCount;
    public TextMeshProUGUI DUCount;

    [SerializeField] GameObject UI_Title;
    [SerializeField] GameObject UI_Battle;

    float start = 0f;
    float end = 1f;
    float time = 0f;

    public float fadeTime = 2f; // 페이드 효과 재생시간
    public bool isPlaying = false; // 페이드 효과가 진행 중임을 체크하는 변수

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player;
        fadeImg = Canvas.transform.Find("Fade Image").gameObject;
        upStateBar = Canvas.transform.Find("UP_StateBar").gameObject;
        UI_Title = Canvas.transform.Find("Title").gameObject;
        UI_Battle = Canvas.transform.Find("Battle").gameObject;
        barCharacterName = upStateBar.transform.Find("CharcterName_Text").GetComponent<TextMeshProUGUI>();
        barHP = upStateBar.transform.Find("HP_Text").GetComponent<TextMeshProUGUI>();
        barGold = upStateBar.transform.Find("Gold_Text").GetComponent<TextMeshProUGUI>();
        //DCount = UI_Battle.transform.Find("D_Text").GetComponent<Text>();
        //DUCount = UI_Battle.transform.Find("DU_Text").GetComponent<Text>();

        fadeImg.SetActive(false);
    }

    public void SetFalse_All()
	{
        UI_Title.SetActive(false);
        UI_Battle.SetActive(false);
	}

    public void SetTure_upStateBar()
	{
        upStateBar.SetActive(true);
    }

    public void SetTrue_Title()
	{
        UI_Title.SetActive(true);
    }

    public void SetTrue_Battle()
    {
        UI_Battle.SetActive(true);
    }

    public void SetUpStateBar()
	{
        barCharacterName.text = player.GetComponent<Player>().P_name;
        barHP.text = GameManager.instance.GM_HP.ToString() + " / " + GameManager.instance.GM_MAXHP.ToString();
        barGold.text = GameManager.instance.gold.ToString();
	}

    public void SetDeckCount()
	{
        DCount.text = GameManager.instance.cardManager.deck_instance.Count.ToString();
        DUCount.text = GameManager.instance.cardManager.deck_Used.Count.ToString();
    }

    public void FadeOut()
	{
        StartCoroutine(FadeOutBackground());
	}

    public void FadeIn()
	{
        StartCoroutine(FadeInBackground());
	}

    // 씬 전환을 위한 코루틴 (페이드 아웃)
    IEnumerator FadeOutBackground()
    {
        fadeImg.SetActive(true);

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

    }

    // 씬 전환을 위한 코루틴 (페이드 인)
    IEnumerator FadeInBackground()
    {
        fadeImg.SetActive(true);

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
        fadeImg.SetActive(false);
    }

    void OnEnable()
    {
        // 델리게이트 체인 추가
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("씬 교체됨, 현재 씬: " + scene.name);

        // 씬 전환 효과 (페이드 인)
        if (scene.name != "Title")
        {
            FadeIn();
        }
        if (scene.name == "GameMap")
        {
            SetFalse_All();
            SetUpStateBar();
            SetTure_upStateBar();
        }
        if (scene.name == "MinorEnemy" || scene.name == "EliteEnemy" || scene.name == "Boss")
        {
            SetFalse_All();
            SetTrue_Battle();
        }
    }

    void OnDisable()
    {
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
