using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestSite : MonoBehaviour
{
    public static RestSite instance;

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

    // 휴식 스테이지 세팅 (HP 회복)
    public void SetRestSite()
    {
        // 체력 회복 (HP 10 회복)
        GameManager.instance.GM_HP += 10;
        GameObject.Find("Scroll View").SetActive(false);
    }

    // 카드 세팅 (휴식 스테이지)
    // 덱의 카드 중 하나를 버릴 것인지 아닌지 선택한다
    public void SetRestSiteCard()
    {
        GameObject.Find("RestSite Panel").transform.Find("Scroll View").gameObject.SetActive(true);

        if (GameObject.Find("_Deck") == null)
        {
            GameObject rDO = new GameObject("_Deck");
            cardAnchor = rDO.transform;
        }

        // 덱 정보를 불러옴 (테스트 용 덱 생성)
        CardManager.instance.InitDeck(CardManager.instance.DeckNum);

        // 덱(카드 프리펩)을 화면에 세팅
        // 현재 전투 씬에서 나가면 덱 리스트의 0~19번 인덱스 정보가 Missing이 되기 때문에 
        for (int i = 20; i < CardManager.instance.Deck_instance.Count; i++)
        {
            //CardManager.instance.Deck_instance[i].transform.parent = cardAnchor;
            CardManager.instance.Deck_instance[i].transform.SetParent(GameObject.Find("Content").transform);
            CardManager.instance.Deck_instance[i].transform.localScale = new Vector3(1f, 1f, 1);
            //CardManager.instance.Deck_instance[i].transform.localPosition = new Vector3((i % 4) * 6.7f - 15f, (i / 4) * -7.3f + 18f, 0);
        }
    }
}
