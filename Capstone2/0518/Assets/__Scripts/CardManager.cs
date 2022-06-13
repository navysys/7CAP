using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    [SerializeField] CardSO cardSO;
    [SerializeField] CardSO storeSO;
    [SerializeField] CardInfo cardInfo;
    [SerializeField] CardInfo storeCardInfo;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Vector3 SpawnPos;
    [SerializeField] Transform leftTr;
    [SerializeField] Transform rightTr;

    [SerializeField] int deckTop;
    [SerializeField] List<Card> deck;
    [SerializeField] List<Card> deck_instance;
    [SerializeField] List<Card> hand;
    [SerializeField] List<Card> deck_Used;
    [SerializeField] List<Card> storeDeck;
    [SerializeField] List<Card> storeDeck_instance;
    [SerializeField] List<Card> treasureDeck;
    [SerializeField] List<Card> treasureDeck_instance;

    [SerializeField] ECardState cardState;

    int deckNum = 20;
    int storeDeckNum = 8;
    int treasureDeckNum = 3;

    Card selectCard;
    bool isMyCardDrag;
    bool onMyCardArea;
    enum ECardState { Nothing, CanMouseOver, CanMouseDrag }

    public List<Card> Deck_instance
    {
        get { return deck_instance; }
        set { deck_instance = value; }
    }

    public List<Card> StoreDeck_instance
    {
        get { return storeDeck_instance; }
        set { storeDeck_instance = value; }
    }

    public List<Card> TreasureDeck_instance
    {
        get { return treasureDeck_instance; }
        set { treasureDeck_instance = value; }
    }

    public int DeckNum
    {
        get { return deckNum; }
        set { deckNum = value; }
    }

    public int StoreDeckNum
    {
        get { return storeDeckNum; }
        set { storeDeckNum = value; }
    }

    public int TreasureDeckNum
    {
        get { return treasureDeckNum; }
        set { treasureDeckNum = value; }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        deck = new List<Card>();
        deck_instance = new List<Card>();
        hand = new List<Card>();
        deck_Used = new List<Card>();
        storeDeck = new List<Card>();
        storeDeck_instance = new List<Card>();
        treasureDeck = new List<Card>();
        treasureDeck_instance = new List<Card>();
        SpawnPos = transform.position;
        SetCardInfo(0);
        SetStoreCardInfo(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
		{
            InitDeck(deckNum);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Draw();
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            InitTreasureDeck(TreasureDeckNum);
        }
        if (isMyCardDrag)
            CardDrag();

        DetectCardArea();
        SetECardState();
    }

	void CardDrag()
	{
		if(!onMyCardArea)
		{
            selectCard.MoveTransform(new PRS(Utils.MousePos, Utils.QI, selectCard.originPRS.scale * 0.8f), false);
		}
	}

    void DetectCardArea()
	{
        RaycastHit2D[] hits = Physics2D.RaycastAll(Utils.MousePos, Vector3.forward);
        int layer = LayerMask.NameToLayer("MyCardArea");
        onMyCardArea = Array.Exists(hits, x => x.collider.gameObject.layer == layer);
        
	}

	GameObject CreateCard()
	{
        var cardObject = Instantiate(cardPrefab, SpawnPos, Utils.QI);
        cardObject.GetComponent<Card>().Setup(cardInfo);
        deck.Add(cardObject.GetComponent<Card>());
        cardObject.GetComponent<Order>().SetOrder(deck.Count);

        return cardObject;
	}

    void CreateStoreCard()
    {
        var cardObject = Instantiate(cardPrefab, SpawnPos, Utils.QI);
        cardObject.GetComponent<Card>().Setup(storeCardInfo);
        storeDeck.Add(cardObject.GetComponent<Card>());
        cardObject.GetComponent<Order>().SetOrder(storeDeck.Count);
    }

    void CreateTreasureCard()
    {
        var cardObject = Instantiate(cardPrefab, SpawnPos, Utils.QI);
        cardObject.GetComponent<Card>().Setup(storeCardInfo);
        treasureDeck.Add(cardObject.GetComponent<Card>());
        cardObject.GetComponent<Order>().SetOrder(treasureDeck.Count);
    }

    void RemoveCard(Card card)
	{
        Destroy(card.gameObject);
	}

    void SetCardInfo(int i)
	{
        cardInfo = cardSO.cards[i];
	}

    void SetStoreCardInfo(int i)
    {
        storeCardInfo = storeSO.cards[i];
    }

    public void InitDeck(int DeckNum)
	{
        for (int i = 0; i < DeckNum; i++)
        {
            //현재 전체 카드 중 랜덤생성 (테스트 다 끝나면 기본덱으로 생성)
            int randomInt = Random.Range(0, cardSO.cards.Length);
            SetCardInfo(randomInt);
            var cardobj = CreateCard();
            cardobj.GetComponent<Order>().SetOriginOrder(i);
        }

        deck_instance = deck.ToList<Card>();
    }

    public void InitStoreDeck(int StoreDeckNum)
    {
        for (int i = 0; i < StoreDeckNum; i++)
        {
            //현재 전체 카드 중 8장을 랜덤 생성 (상점용)
            int randomInt = Random.Range(0, storeSO.cards.Length);
            SetStoreCardInfo(randomInt);
            CreateStoreCard();
            storeDeck[i].name = "StoreCard " + (i +1);
        }

        storeDeck_instance = storeDeck.ToList<Card>();
    }

    public void InitTreasureDeck(int TreasureDeckNum)
    {
        for (int i = 0; i < TreasureDeckNum; i++)
        {
            //현재 전체 카드 중 8장을 랜덤 생성 (상점용)
            int randomInt = Random.Range(0, storeSO.cards.Length);
            SetStoreCardInfo(randomInt);
            CreateTreasureCard();
            treasureDeck[i].name = "TreasureCard " + (i + 1);
        }

        treasureDeck_instance = treasureDeck.ToList<Card>();
    }

    public void Draw()
	{
        hand.Add(deck_instance[0]);
        deck_instance.RemoveAt(0);
        CardAlignment();
	}

    public void UseCard()
	{
        Debug.Log("UseCard debug");
	}

    void CardAlignment()
	{
        List<PRS> originCardPRSs = new List<PRS>();
        originCardPRSs = RoundAlignment(leftTr, rightTr, hand.Count, 0.5f, Vector3.one * 0.85f) ;

        var targetCards = hand;
        for(int i = 0; i < targetCards.Count; i++)
		{
            var targetCard = targetCards[i];

            targetCard.originPRS = originCardPRSs[i];
            targetCard.MoveTransform(targetCard.originPRS, true, 0.7f);
		}
	}

	List<PRS> RoundAlignment(Transform leftTr, Transform rightTr, int objCount, float height, Vector3 scale)
	{
        float[] objLerps = new float[objCount];
        List<PRS> results = new List<PRS>(objCount);

        switch(objCount)
		{
            case 1: objLerps = new float[] { 0.5f }; break;
            case 2: objLerps = new float[] { 0.27f, 0.73f }; break;
            case 3: objLerps = new float[] { 0.1f, 0.5f, 0.9f }; break;
            default:
                float interval = 1f / (objCount - 1);
                for(int i = 0; i < objCount; i++)
				{
                    objLerps[i] = interval * i;
				}
                break;
        }

        for(int i =0; i < objCount; i++)
		{
            var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);
            var targetRot = Utils.QI;
            if(objCount >= 4)
			{
                float curve = Mathf.Sqrt(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2));
                curve = height >= 0 ? curve : -curve;
                targetPos.y += curve;
                targetRot = Quaternion.Slerp(leftTr.rotation, rightTr.rotation, objLerps[i]);
			}
            results.Add(new PRS(targetPos, targetRot, scale));
		}
        return results;
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
            InitDeck (deckNum);
            for(int i = 0; i < 5; i++)
			{
                Draw();
            }
            
        }
    }

    void OnDisable()
    {
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

	#region MyCard

    public void CardMouseOver(Card card)
	{
        if(cardState == ECardState.Nothing)
            return;

        selectCard = card;
        EnlargeCard(true, card);
	}

    public void CardMouseExit(Card card)
	{
        EnlargeCard(false, card);
	}

    public void CardMouseDown()
	{
        if (cardState != ECardState.CanMouseDrag)
            return;

        isMyCardDrag = true;
	}

    public void CardMouseUp()
	{
        isMyCardDrag=false;

        if(cardState != ECardState.CanMouseDrag)
            return;

		if (!onMyCardArea)
		{
            UseCard();
        }
	}

    void EnlargeCard(bool isEnlarge, Card card)
	{
        if (isEnlarge)
        {
            Vector3 enlargePos = new Vector3(card.originPRS.pos.x, 8.5f, -2f);
            card.MoveTransform(new PRS(enlargePos, Utils.QI, Vector3.one * 1.0f), false);
        }
        else
            card.MoveTransform(card.originPRS, false);

        card.GetComponent<Order>().SetMostFrontOrder(isEnlarge);
	}

    void SetECardState()
    {
        if (GameManager.instance.isLoading)
            cardState = ECardState.Nothing;

        else if (!GameManager.instance.myTurn)
            cardState = ECardState.CanMouseOver;

        else if (GameManager.instance.myTurn)
            cardState = ECardState.CanMouseDrag;
    }

    #endregion
}
