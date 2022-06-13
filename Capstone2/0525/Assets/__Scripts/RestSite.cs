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

    // �޽� �������� ���� (HP ȸ��)
    public void SetRestSite()
    {
        // ü�� ȸ�� (HP 10 ȸ��)
        GameManager.instance.GM_HP += 10;
        GameObject.Find("Scroll View").SetActive(false);
    }

    // ī�� ���� (�޽� ��������)
    // ���� ī�� �� �ϳ��� ���� ������ �ƴ��� �����Ѵ�
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
