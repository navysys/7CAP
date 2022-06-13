using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    // ���� ���� ��ũ��Ʈ
    // ������ �����ϸ� ī�带 8~10�� �������� �̾Ƽ� ��ġ
    // ���ݵ� �������� ����
    public GameObject cardPrefab;   // ī�� ������

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

        // ���� : ī�� ����Ʈ���� 8~10���� �������� �̾Ƽ� ���� ȭ�鿡 ����
        // �׽�Ʈ�� ���� �ϴ� 8�� ����
        for (int i = 0; i < 8; i++)
        {
            cardList.Add(MakeCards(i));
        }
    }

    // ī�� ���� �޼���
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
    // ��带 �� �Ҹ��ϸ� ī�带 ��Ȱ��ȭ��Ŵ
    private void CloseCard()
    {
        GameObject cardObj = GameObject.Find("CardPrefab(Clone)");
    }
    */
}
