using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    // ���� ���� ��ũ��Ʈ
    // ������ �����ϸ� ī�带 8~10�� �������� �̾Ƽ� ��ġ
    public Text priceText;
    public Transform storeCardsAnchor;
    public RectTransform priceAnchor;

    void Start()
    {
        GameManager.instance.GetGold();
        SetStore();
    }

    // ���� ����
    void SetStore()
    {
        if (GameObject.Find("_StoreDeck") == null)
        {
            GameObject sDO = new GameObject("_StoreDeck");
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
            var priceObject = Instantiate(priceText);
            RectTransform pRT = priceObject.GetComponent<RectTransform>();
            priceObject.transform.SetParent(priceAnchor);
            priceObject.text = CardManager.instance.StoreDeck_instance[i].cardInfo.price.ToString() + " G";
            pRT.anchoredPosition = new Vector2((i % 4) * 415f - 715f, (i / 4) * -450f);
        }
    }

    /*
    // ��带 �� �Ҹ��ϸ� ī�带 ��Ȱ��ȭ��Ŵ
    void CloseCard()
    {

    }
    */
}
