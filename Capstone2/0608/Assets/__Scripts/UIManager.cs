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

    public float fadeTime = 2f; // ���̵� ȿ�� ����ð�
    public bool isPlaying = false; // ���̵� ȿ���� ���� ������ üũ�ϴ� ����

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

    // �� ��ȯ�� ���� �ڷ�ƾ (���̵� �ƿ�)
    IEnumerator FadeOutBackground()
    {
        fadeImg.SetActive(true);

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

    }

    // �� ��ȯ�� ���� �ڷ�ƾ (���̵� ��)
    IEnumerator FadeInBackground()
    {
        fadeImg.SetActive(true);

        Color bgColor = fadeImg.GetComponent<Image>().color;
        start = 1f;
        end = 0f;
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
        fadeImg.SetActive(false);
    }

    void OnEnable()
    {
        // ��������Ʈ ü�� �߰�
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("�� ��ü��, ���� ��: " + scene.name);

        // �� ��ȯ ȿ�� (���̵� ��)
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
        // ��������Ʈ ü�� ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
