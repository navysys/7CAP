using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    // 상점 관련 스크립트
    // 상점에 진입하면 카드를 8~10장 렌덤으로 뽑아서 배치
    // 가격도 렌덤으로 설정
    public GameObject cardPrefab;   // 카드 프리펩

    public Transform storeCardsAnchor;

    List<Card> cardList = new List<Card>();

    void Start()
    {
        SetStore();
    }

    public void SetStore()
    {
        if (GameObject.Find("StoreCards") == null)
        {
            GameObject anchorGO = new GameObject("StoreCards");
            storeCardsAnchor = anchorGO.transform;
        }

        // 목적 : 카드 리스트에서 8~10장을 렌덤으로 뽑아서 상점 화면에 세팅
        // 테스트를 위해 일단 8장 세팅
        for (int i = 0; i < 8; i++)
        {
            cardList.Add(MakeCards(i));
        }
    }

    // 카드 생성 메서드
    private Card MakeCards(int cNum)
    {
        GameObject cpo = Instantiate(cardPrefab) as GameObject;
        cpo.transform.parent = storeCardsAnchor;
        Card card = cpo.GetComponent<Card>();

        cpo.transform.localScale = new Vector3(0.7f, 0.7f, 1);
        cpo.transform.localPosition = new Vector3((cNum % 4) * 6.7f - 15f, (cNum / 4) * -7.3f + 18f, 0);

        return card;
    }
    /*
    // 골드를 다 소모하면 카드를 비활성화시킴
    private void CloseCard()
    {
        GameObject cardObj = GameObject.Find("CardPrefab(Clone)");
    }
    */
}
