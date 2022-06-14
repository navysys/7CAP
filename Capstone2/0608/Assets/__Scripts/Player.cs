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
    public bool isResetDef = false;

    public HP_Bar bar;
    public Shield shield;

	private void Start()
	{
        bar.SetHp(HP, MaxHP);
	}

	public void AddDef(int i)
	{
        Def += i;
        shield.SetShield(Def);
    }

    public void ResetDef()
	{
        if(isResetDef)
        {
            Def = 0;
            shield.SetShield(Def);
        }
            
        shield.SetShield(Def);
    }

    public void ReduceHP(int i)
	{
        if(Def > 0)
		{
            Def -= i; 
            if (Def > 0)
			{
                return;
			}
			else
			{
                i = -(Def);
                Def = 0;
			}
		}
        HP -= i;
        transform.GetComponent<SetDamageText>().TakeDamage(i);

        if (HP <= 0)
		{
            HP = 0;
            //animator 죽는모션 추가해야함
            GameManager.instance.GameOver();
		}
        bar.SetHp(HP, MaxHP);
        shield.SetShield(Def);
        GameManager.instance.GM_HP = HP;
        GameManager.instance.uiManager.SetUpStateBar();
    }
}
