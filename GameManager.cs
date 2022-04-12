using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<Relic> RelicList;

    public GameObject Player;
    public GameObject EnemySpawner;
    public Deck deck;
    
    enum GameState { Start, Map, Battle };
    GameState State;

    enum BattleState { PlayerTurn, EnemyTurn };
    BattleState BState;

    int GM_MAXHP;
    int GM_HP;
    
    public void GameOver()
	{
        //게임오버 ui 표시 버튼 누르면 처음으로
        Debug.Log("게임오버");
	}

    public void BE_GameStart()
	{
        SceneManager.LoadScene("Battle");
	}

    void StateCheck()
	{
		switch (State)
		{
			case GameState.Start:
                //이전 데이터 확인
				break;
			case GameState.Map:
                // UI 갱신 필요한것
				break;
			case GameState.Battle:
                BeforeBattleSetting();
                BattleStateCheck();
                break;
			default:
				break;
		}
	}

    void BattleStateCheck()
	{
		switch (BState)
		{
			case BattleState.PlayerTurn:
                //코스트 초기화
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

    void BeforeBattleSetting()
	{
        SetPlayerAtt();
    }

    void SetPlayerAtt()
	{
        Player player = Player.GetComponent<Player>();

        player.MaxHP = GM_MAXHP;
        player.HP = GM_HP;

	}

    void Awake()
    {
        if(instance == null)
		{
            instance = this;
        }
		else
		{
            Destroy(gameObject);
		}
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
