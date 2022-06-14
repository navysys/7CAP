using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
	public void C_Effect(int CType, int Value, int Range = 1, int CostHP = 0, bool isKill = false, int draw = 0)
	{
		switch (CType)
		{
			case 0:		//공격
				Attack(Value, Range, CostHP, isKill, draw);
				break;
			case 1:     //실드
				Shield(Value);
				break;
			case 2:		//hp 회복
				Heal(Value);
				break;

		}
	}

	void Attack(int Damage, int Range, int CostHP, bool isKill, int draw)
	{
		if(CostHP != 0)
		{
			Player_ReduceHP(CostHP);
		}

		if(Range == 1)
		{
			bool killed = Enemy_ReduceHP(Damage);

			if (killed && isKill)
			{
				Card_Draw();
			}
		}
	}

	void Shield(int SNum)
	{
		GameManager.instance.player.GetComponent<Player>().AddDef(SNum);
	}

	void Heal(int HNum)
	{
		GameManager.instance.player.GetComponent<Player>().HP += HNum;
	}

	void Player_ReduceHP(int Cost)
	{
		GameManager.instance.player.GetComponent<Player>().ReduceHP(Cost);
	}

	bool Enemy_ReduceHP(int Damage)
	{
		return GameManager.instance.enemySpawner.enemyList[0].ReduceHP(Damage);
	}

	void Card_Draw()
	{
		GameManager.instance.cardManager.Draw();
	}
}
