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

        //GameManager.instance.cardManager.SetTreasureCard();
    }

    // ī�� ���� (���� ��������)
    // ���� ���� ī�� �� 3���� �������� �̾Ƽ� ��ġ
}
