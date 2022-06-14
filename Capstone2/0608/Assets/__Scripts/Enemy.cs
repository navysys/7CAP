using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
		transform.DOMoveX(transform.position.x + 0.5f, 0.15f).SetLoops(2, LoopType.Yoyo);
		transform.GetComponent<SetDamageText>().TakeDamage(i);


		if (HP <= 0)
		{
			HP = 0;
			//animator 죽는모션 추가해야함
			int ran = Random.Range(20, 60);
			transform.GetComponent<SetDamageText>().GetGold(ran);
			GameManager.instance.gold += ran;
			GameManager.instance.uiManager.SetUpStateBar();
			Destroy(gameObject, 0.2f);
			return true;
		}		
		bar.SetHp(HP, maxHP);
		return false;
	}

	public void Action()
	{
		transform.DOMoveX(transform.position.x -1, 0.3f).SetLoops(2, LoopType.Yoyo);
		GameManager.instance.player.GetComponent<Player>().ReduceHP(power);
	}

	private void OnDestroy()
	{
		GameManager.instance.enemySpawner.ReduceEnemyCount(this);
	}
}
