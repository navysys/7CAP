using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
	public void CardType(int CType, int Value, int Range = 1, Enemy e = null, int CostHP = 0, bool isKill = false, int draw = 0)
	{
		switch (CType)
		{
			case 0:		//공격
				Attack(Value, Range, e, CostHP, isKill, draw);
				break;
			case 1:     //실드
				Shield(Value);
				break;
			case 2:		//hp 회복
				Heal(Value);
				break;

		}
	}

	void Attack(int Damage, int Range, Enemy e, int CostHP, bool isKill, int draw)
	{
		if(CostHP != 0)
		{
			GameManager.instance.player.GetComponent<Player>().ReduceHP(CostHP);
		}

		if(Range == 1)
		{
			Debug.Log("range" + Range);
			if(e.ReduceHP(Damage) || isKill == true)
			{
				if (draw != 0)
				{
					GameManager.instance.cardManager.Draw();
					GameManager.instance.cardManager.BonusDraw();
				}
			}
		}
		else if(Range == 2)
		{
			Debug.Log("2명 공격");
		}	
	}

	void Shield(int SNum)
	{
		GameManager.instance.player.GetComponent<Player>().Def = GameManager.instance.player.GetComponent<Player>().Def + SNum;
	}

	void Heal(int HNum)
	{
		GameManager.instance.player.GetComponent<Player>().HP = GameManager.instance.player.GetComponent<Player>().HP + HNum;
	}
}
