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

    // 보물 스테이지 세팅
    public void SetTreasure()
    {
        if (GameObject.Find("_TreasureDeck") == null)
        {
            GameObject tDO = new GameObject("_TreasureDeck");
            cardAnchor = tDO.transform;
        }

        //GameManager.instance.cardManager.SetTreasureCard();
    }

    // 카드 세팅 (보물 스테이지)
    // 상점 덱의 카드 중 3개를 렌덤으로 뽑아서 배치
}
