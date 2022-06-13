using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] SpriteRenderer image;
	[SerializeField] int maxHP;
	[SerializeField] int power;
	[SerializeField] int HP;

	public EnemyInfo enemyinfo;
	public HP_Bar bar;

	private void Start()
	{
		bar.SetHp(this.HP, this.maxHP);
		GameManager.instance.enemySpawner.SortingPos();
	}

	public void Setup(EnemyInfo info)
	{
		enemyinfo = info;

		image.sprite = enemyinfo.image;
		maxHP = enemyinfo.HP;
		power = enemyinfo.Power;
		HP = maxHP;
	}

	public bool ReduceHP(int i)
	{
		HP -= i;
		if (HP <= 0)
		{
			HP = 0;
			//animator 죽는모션 추가해야함

			Destroy(gameObject, 0.2f);
			return true;
		}		
		bar.SetHp(HP, maxHP);
		return false;
	}

	public void Action()
	{
		GameManager.instance.player.GetComponent<Player>().ReduceHP(power);
	}

	private void OnDestroy()
	{
		GameManager.instance.enemySpawner.ReduceEnemyCount(this);
	}
}
