using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDamageText : MonoBehaviour
{
    public GameObject hudDamageText;
    public GameObject hudGoldText;
    public Transform hudPos;

    void Start()
	{
        hudPos = transform;
    }

    public void TakeDamage(int damage)
    {
        GameObject hudText = Instantiate(hudDamageText); // 생성할 텍스트 오브젝트
        hudText.transform.position = hudPos.position; // 표시될 위치
        hudText.GetComponent<DamageText>().damage = damage; // 데미지 전달
        hudPos = transform;
    }

    public void GetGold(int gold)
	{
        GameObject hudText = Instantiate(hudGoldText); // 생성할 텍스트 오브젝트
        hudText.transform.position = new Vector3(hudPos.position.x, hudPos.position.y + 1, hudPos.position.z); // 표시될 위치
        hudText.GetComponent<GoldText>().gold = gold; // 데미지 전달
        hudPos = transform;
    }
}
