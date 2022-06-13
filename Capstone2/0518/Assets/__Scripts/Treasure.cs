using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Treasure : MonoBehaviour
{
    public static Treasure instance;

    public Transform cardAnchor;

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

    // ���� �������� ����
    public void SetTreasure()
    {
        if (GameObject.Find("_TreasureDeck") == null)
        {
            GameObject tDO = new GameObject("_TreasureDeck");
            cardAnchor = tDO.transform;
        }

        SetTreasureCard();
    }

    // ī�� ���� (���� ��������)
    // ���� ���� ī�� �� 3���� �������� �̾Ƽ� ��ġ
    void SetTreasureCard()
    {
        CardManager.instance.InitTreasureDeck(CardManager.instance.TreasureDeckNum);

        for (int i = 0; i < CardManager.instance.TreasureDeck_instance.Count; i++)
        {
            CardManager.instance.TreasureDeck_instance[i].transform.parent = cardAnchor;
            CardManager.instance.TreasureDeck_instance[i].transform.localScale = new Vector3(0.6f, 0.6f, 1);
            CardManager.instance.TreasureDeck_instance[i].transform.localPosition = new Vector3((5 * i) - 5, -1, 0);
        }
    }
}
