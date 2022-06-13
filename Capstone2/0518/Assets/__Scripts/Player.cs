using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public string P_name = "Warrior";
    public int MaxHP = 90;
    public int HP = 90;
    public int Def;

    public HP_Bar bar;

    public void AddDef(int i)
	{
        Def += i;
	}

    public void ResetDef()
	{
        Def = 0;
	}

    public void ReduceHP(int i)
	{
        HP -= i;
        if(HP <= 0)
		{
            HP = 0;
            //animator 죽는모션 추가해야함
            GameManager.instance.GameOver();
		}
        bar.SetHp(HP, MaxHP);
        GameManager.instance.GM_HP = HP;
        GameManager.instance.uiManager.SetUpStateBar();
    }
}
