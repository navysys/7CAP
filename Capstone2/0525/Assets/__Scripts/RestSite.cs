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
        SetRestSiteCard();
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

        SetRestSite();
    }
}
