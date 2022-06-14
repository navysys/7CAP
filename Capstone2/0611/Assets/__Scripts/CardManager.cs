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
    [SerializeField] CardSO warriorSO;
    [SerializeField] CardSO storeSO;
    [SerializeField] CardInfo cardInfo;

    [SerializeField] GameObject cardPrefab;
    [SerializeField] Vector3 SpawnPos;
    [SerializeField] Transform leftTr;
    [SerializeField] Transform rightTr;

    [SerializeField] int deckTop;

    [SerializeField] List<CardInfo> deckInfo;
    [SerializeField] List<Card> deck;
    public List<Card> deck_instance;
    [SerializeField] List<Card> hand;
    public List<Card> deck_Used;
    [SerializeField] List<Card> storeDeck;
    [SerializeField] List<Card> storeDeck_instance;
    [SerializeField] List<Card> treasureDeck;
    [SerializeField] List<Card> treasureDeck_instance;
    [SerializeField] ECardState cardState;

    int deckNum = 0;
    int storeDeckNum = 8;
    int treasureDeckNum = 3;
    public int UCCount = 0;

    Card selectCard;
    bool isMyCardDrag;
    bool onMyCardArea;
    public bool isBattle = false;
    public bool isTreasure = false;

    enum ECardState { Nothing, CanMouseOver, CanMouseDrag }

    public List<Card> StoreDeck_instance
    {
        get { return storeDeck_instance; }
        set { storeDeck_instance = value; }
    }

    public int StoreDeckNum
    {
        get { return storeDeckNum; }
        set { storeDeckNum = value; }
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
    }

    // Update is called once per frame
    void Update()
    {
        if (isMyCardDrag)
            CardDrag();

        DetectCardArea();
        SetECardState();
    }

    #region CreateCard

    GameObject CreateCard(int cardType, int i)
	{
        SetCardInfo(cardType, i);
        var cardObject = Instantiate(cardPrefab, SpawnPos, Utils.QI);
        cardObject.GetComponent<Card>().Setup(cardInfo);
        deck_instance.Add(cardObject.GetComponent<Card>());
        cardObject.GetComponent<Order>().SetOriginOrder(deck_instance.Count);
        deckNum++;

        return cardObject;
	}

    GameObject CopyCard()
    {
        var cardObject = Instantiate(cardPrefab, SpawnPos, Utils.QI);
        cardObject.GetComponent<Card>().Setup(cardInfo);
        deck_instance.Add(cardObject.GetComponent<Card>());
        cardObject.GetComponent<Order>().SetOriginOrder(deck_instance.Count);
        deckNum++;

        return cardObject;
    }

    GameObject CopyCardTest()
    {
        var cardObject = Instantiate(cardPrefab, SpawnPos, Utils.QI);
        cardObject.GetComponent<Card>().Setup(cardInfo);
        cardObject.AddComponent<RectTransform>();
        deck_instance.Add(cardObject.GetComponent<Card>());
        cardObject.GetComponent<Order>().SetOriginOrder(deck_instance.Count);
        deckNum++;

        return cardObject;
    }

    void CreateDeckInfo(int cardType, int i)
	{
        SetCardInfo(cardType, i);
        deckInfo.Add(cardInfo);
	}

    void CreateStoreCard()
    {
        var cardObject = Instantiate(cardPrefab, SpawnPos, Utils.QI);
        cardObject.GetComponent<Card>().Setup(cardInfo);
        storeDeck.Add(cardObject.GetComponent<Card>());
        cardObject.GetComponent<Order>().SetOrder(storeDeck.Count);
    }

    void CreateTreasureCard()
    {
        var cardObject = Instantiate(cardPrefab, SpawnPos, Utils.QI);
        cardObject.GetComponent<Card>().Setup(cardInfo);
        treasureDeck.Add(cardObject.GetComponent<Card>());
        cardObject.GetComponent<Order>().SetOrder(treasureDeck.Count);
    }

    #endregion

    #region InitDeck
    public void InitWarriorDeck()
	{
        CreateDeckInfo(0, 0);
        CreateDeckInfo(0, 0);
        CreateDeckInfo(0, 0);
        CreateDeckInfo(0, 0);
        CreateDeckInfo(0, 1);
        CreateDeckInfo(0, 1);
        CreateDeckInfo(0, 1);
        CreateDeckInfo(0, 1);
        CreateDeckInfo(1, 0);
        CreateDeckInfo(1, 0);
        CreateDeckInfo(1, 1);
        CreateDeckInfo(1, 1);
        CreateDeckInfo(1, 2);
        CreateDeckInfo(1, 2);
        CreateDeckInfo(1, 3);
    }

    public void InitDeckInstance()
	{
        for (int i = 0; i < deckInfo.Count; i++)
		{
            cardInfo = deckInfo[i];
            CopyCard();
		}
	}

    public void InitDeckInstanceTest()
    {
        for (int i = 0; i < deckInfo.Count; i++)
        {
            cardInfo = deckInfo[i];
            CopyCardTest();
        }
    }

    public void SuffleDeck()
	{
        List<Card> cards = new List<Card>();
        cards= deck_instance.ToList<Card>();
        deck_instance.Clear();

        for (int i = 0; i < 15; i++)
        {
            int Randomint = Random.Range(0, cards.Count);
            deck_instance.Add(cards[Randomint]);
            cards.RemoveAt(Randomint);                                  //리스트의 카드를 따로 생성 따로 생성된 카드를 초기화 등등
        }
    }

    public void InitStoreDeck(int StoreDeckNum)
    {
        for (int i = 0; i < StoreDeckNum; i++)
        {
            //현재 전체 카드 중 8장을 랜덤 생성 (상점용)
            int randomInt = Random.Range(0, storeSO.cards.Length);
            SetCardInfo(0, randomInt);
            CreateStoreCard();
            storeDeck[i].name = "StoreCard " + (i +1);
        }

        storeDeck_instance = storeDeck.ToList<Card>();
    }

    public void InitTreasureDeck(int TreasureDeckNum)
    {
        for (int i = 0; i < TreasureDeckNum; i++)
        {
            //현재 전체 카드 중 3장을 랜덤 생성 (보물용)
            int randomInt = Random.Range(0, storeSO.cards.Length);
            SetCardInfo(0, randomInt); ;
            CreateTreasureCard();
            treasureDeck[i].name = "TreasureCard " + (i + 1);
        }

        treasureDeck_instance = treasureDeck.ToList<Card>();
    }

    public void ClearDeck()
	{
        deck_instance = new List<Card>();
        hand = new List<Card>();
        deck_Used = new List<Card>();
	}
    #endregion

    #region SetCardMovement

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

    void CardDrag()
    {
        if (!onMyCardArea)
        {
            selectCard.MoveTransform(new PRS(Utils.MousePos, Utils.QI, selectCard.originPRS.scale * 0.5f), false);
        }
    }

    void DetectCardArea()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Utils.MousePos, Vector3.forward);
        int layer = LayerMask.NameToLayer("MyCardArea");
        onMyCardArea = Array.Exists(hits, x => x.collider.gameObject.layer == layer);

    }

/*    Enemy DetectEnemy()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Utils.MousePos, Vector3.forward);
        int layer = LayerMask.NameToLayer("Enemy");
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.layer == layer)
            {
                return hits[i].collider.GetComponent<Enemy>();
            }
        }
        return null;
    }*/

    #endregion

    #region SetCardRule

    public void Draw()
	{
        deck_instance[0].GetComponent<Order>().SetOriginOrder(hand.Count);
        hand.Add(deck_instance[0]);
        deck_instance.RemoveAt(0);
        CardAlignment();
        GameManager.instance.uiManager.SetDeckCount();
    }

    public void BonusDraw()
	{
        UCCount++;
        if(UCCount >= 3)
		{
            Draw();
            UCCount = 0;
        }
	}

    public void UseCard()
	{
        selectCard.effect();

        deck_Used.Add(selectCard);
        hand.Remove(selectCard);
        selectCard.gameObject.SetActive(false);
        BonusDraw();
        CardAlignment();
        GameManager.instance.uiManager.SetDeckCount();
    }

    void RemoveCard(Card card)
    {
        Destroy(card.gameObject);
    }

    void SetCardInfo(int cardType, int i)
    {
        if (cardType == 0)
        {
            cardInfo = cardSO.cards[i];
        }
        else if (cardType == 1)
        {
            cardInfo = warriorSO.cards[i];
        }
    }

    public void ResetUCCount()
	{
        UCCount = 0;
	}

    #endregion

    #region Del
    void OnEnable()
    {
        // 델리게이트 체인 추가
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MinorEnemy" || scene.name == "EliteEnemy" || scene.name == "Boss")
        {
            isBattle = true;
            UCCount = 0;
            if(deckInfo.Count == 0)
                InitWarriorDeck();

            InitDeckInstance();
            //InitDeckInstanceTest();
            SuffleDeck();

			for (int i = 0; i <= 3; i++)
			{
				Draw();
			}
		}
        else
		{
            isBattle = false;
            ClearDeck();
        }
    }

    void OnDisable()
    {
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    #endregion

    #region MyCard

    public void CardMouseOver(Card card)
	{
        if(cardState == ECardState.Nothing)
            return;

        if(isBattle)
        {
            selectCard = card;
            EnlargeCard(true, card);
        }
	}

    public void CardMouseExit(Card card)
	{
        if (isBattle)
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

    public void SetRestSite()
    {
        // 테스트 용 덱 생성.
        InitDeckInstanceTest();

        // 덱(카드 프리펩)을 화면에 세팅.
        // 카드 프리펩의 Transform을 RectTransform으로 교체해야 스크롤 뷰가 작동됨.
        // 그러나 스크롤 뷰는 이미지 컴포넌트가 적용된 오브젝트에서만 뷰 영역에서만 표시가 되는 등 정상적으로 작동되지만,
        // 카드 프리펩은 스프라이트 렌더러 컴포넌트가 적용되어 있어 뷰 영역 바깥에서도 표시가 되는 문제가 존재함.
        for (int i = 0; i < deck_instance.Count; i++)
        {
            deck_instance[i].transform.SetParent(GameObject.Find("Content").transform);
            deck_instance[i].transform.localScale = new Vector3(20f, 20f, 1);
        }

        // 덱 정보 초기화
        // ClearDeck();
    }
    
    public void SetTreasure()
    {
        isTreasure = true;

        InitTreasureDeck(treasureDeckNum);

        for (int i = 0; i < treasureDeck_instance.Count; i++)
        {
            treasureDeck_instance[i].transform.localScale = new Vector3(0.7f, 0.7f, 1);
            treasureDeck_instance[i].transform.localPosition = new Vector3((5 * i) - 5, -0.5f, 0);
        }
    }

    public void ClearTreasure()
    {
        if (!isTreasure)
        {
            for (int i = 0; i < treasureDeck.Count; i++)
            {
                Destroy(treasureDeck[i].gameObject);
            }
            
            treasureDeck.Clear();
            treasureDeck_instance.Clear();
        }
    }

    public void ClearStore()
    {
        storeDeck.Clear();
        storeDeck_instance.Clear();
    }

    public void AddCardToDeckInfo(CardInfo cardinfo)
	{
        deckInfo.Add(cardinfo);
	}
    
}
