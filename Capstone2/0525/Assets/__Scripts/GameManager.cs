using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Manager Scripts")]
    public SceneDirector sceneDirector;
    public UIManager uiManager;
    public EnemySpawner enemySpawner;
    public CardManager cardManager;

    [Header("Player Value")]
    public GameObject player;

    public int GM_MAXHP;
    public int GM_HP;
    public int gold;
    public bool myTurn;
    public bool isLoading;  // true 라면 카드와 엔티티 클릭방지용

    enum BattleState { PlayerTurn, EnemyTurn };
    [SerializeField]
    BattleState BState;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        gold = 1000;
    }

    void SetPlayerAtt()
	{
        player = GameObject.Find("Player");

        player.GetComponent<Player>().MaxHP = GM_MAXHP;
        player.GetComponent<Player>().HP = GM_HP;

	}

    void BattleStateCheck()
	{
		switch (BState)
		{
			case BattleState.PlayerTurn:
                //덱초기화
                //카드 활성화
                break;
			case BattleState.EnemyTurn:
                // 적 AI 실행
                // 다음 공격 예고하는 UI 갱신
				break;
			default:
				break;
		}
	}

	public void SelectPlayer(int PlayerNum)
    {
        switch (PlayerNum)
        {
            case 1:
                GM_MAXHP = 100;
                GM_HP = GM_MAXHP;
                break;
            case 2:
                GM_MAXHP = 110;
                GM_HP = GM_MAXHP;
                break;
            case 3:
                GM_MAXHP = 90;
                GM_HP = GM_MAXHP;
                break;

        }
    }

    public void GameOver()
	{
        //게임오버 ui 표시 버튼 누르면 처음으로
        Debug.Log("게임오버");
	}

    public void ExitGame()
	{
        Application.Quit();
	}

    public void EndTurn()
	{
        BState = BattleState.EnemyTurn;
        myTurn = false;
        enemySpawner.EnemyAction();

        StartCoroutine(StartTurnCo());
	}

    public void BattleSetup()
	{
        SetPlayerAtt();
        BState = BattleState.PlayerTurn;
        myTurn = true;
    }

    public void StoreCardClicked(Card card)
    {
        BuyCard(card);
    }

    public void GetGold()
    {
        if (GameObject.Find("Remainder Gold"))
        {
            GameObject obj = GameObject.Find("Remainder Gold");
            Text objText = obj.GetComponent<Text>();
            objText.text = "남은 골드\n" + gold.ToString() + " G";
        }
    }

    public void BuyCard(Card card)
    {
        if (gold > 0)
        {
            gold -= card.cardInfo.price;
            GameObject obj = GameObject.Find("Remainder Gold");
            Text objText = obj.GetComponent<Text>();
            objText.text = "남은 골드\n" + gold.ToString() + " G";
            Debug.Log(gold);
        }
    }

    public void RecoveryHP()
    {
        // 체력 회복
        GM_HP += 10;
        GameObject.Find("Scroll View").SetActive(false);

    }

    public IEnumerator StartTurnCo()
	{
        isLoading = true;

        yield return new WaitForSeconds(0.5f);
        cardManager.Draw();
        yield return new WaitForSeconds(0.5f);

        BState = BattleState.PlayerTurn;
        myTurn = true;
        isLoading = false;
    }

    void OnEnable()
    {
        // 델리게이트 체인 추가
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MinorEnemy" || scene.name == "EliteEnemy" || scene.name == "Boss")
        {
            BattleSetup();
        }
    }

    void OnDisable()
    {
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
