using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    // ���� ���� ��ũ��Ʈ
    // ������ �����ϸ� ī�带 10�� �������� �̾Ƽ� ��ġ
    public static StoreManager instance;

    [SerializeField] public Text priceText;
    [SerializeField] public Transform storeCardsAnchor;
    [SerializeField] public UIManager uIManager;

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

    void Start()
    {
        //GameManager.instance.GetGold();
        //SetStore();
    }

    // ���� ����
    public void SetStore()
    {
        if (GameObject.Find("StoreDeck") == null)
        {
            GameObject sDO = new GameObject("StoreDeck");
            storeCardsAnchor = sDO.transform;
        }

        SetCard();
        SetPrice();
    }

    // ī�� ����
     void SetCard()
    {
        CardManager.instance.InitStoreDeck(CardManager.instance.StoreDeckNum);

        for (int i = 0; i < CardManager.instance.StoreDeck_instance.Count; i++)
        {
            CardManager.instance.StoreDeck_instance[i].transform.parent = storeCardsAnchor;
            CardManager.instance.StoreDeck_instance[i].transform.localScale = new Vector3(0.7f, 0.7f, 1);
            CardManager.instance.StoreDeck_instance[i].transform.localPosition = new Vector3((i % 4) * 6.7f - 15f, (i / 4) * -7.3f + 18f, 0);
        }
    }

    // ����ǥ ����
    void SetPrice()
    {
        for (int i = 0; i < CardManager.instance.StoreDeck_instance.Count; i++)
        {
            Text priceObject = Instantiate(priceText);
            RectTransform pRT = priceObject.GetComponent<RectTransform>();
            uIManager.SetPriceAnchor(priceObject);
            priceObject.text = CardManager.instance.StoreDeck_instance[i].cardInfo.price.ToString() + " G";
            pRT.anchoredPosition = new Vector2((i % 4) * 415f - 715f, (i / 4) * -450f);
        }
    }

    // ����ǥ ����
    public void ClearStorePrice()
    {
        if (GameObject.Find("StorePriceTag") != null)
        {
            GameObject tGO = GameObject.Find("StorePriceTag");
            int nSize = tGO.transform.childCount;

            for (int i = 0; i < nSize; i++)
                Destroy(tGO.transform.GetChild(i).gameObject);
        }
    }

    /*
    // ��带 �� �Ҹ��ϸ� ī�带 ��Ȱ��ȭ��Ŵ
    void CloseCard()
    {

    }
    */
}
