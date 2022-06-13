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

        // �� ������ �ҷ��� (�׽�Ʈ �� �� ����)
        CardManager.instance.InitDeck(CardManager.instance.DeckNum);

        // ��(ī�� ������)�� ȭ�鿡 ����
        // ���� ���� ������ ������ �� ����Ʈ�� 0~19�� �ε��� ������ Missing�� �Ǳ� ������ 
        for (int i = 20; i < CardManager.instance.Deck_instance.Count; i++)
        {
            //CardManager.instance.Deck_instance[i].transform.parent = cardAnchor;
            CardManager.instance.Deck_instance[i].transform.SetParent(GameObject.Find("Content").transform);
            CardManager.instance.Deck_instance[i].transform.localScale = new Vector3(1f, 1f, 1);
            //CardManager.instance.Deck_instance[i].transform.localPosition = new Vector3((i % 4) * 6.7f - 15f, (i / 4) * -7.3f + 18f, 0);
        }
    }
}
