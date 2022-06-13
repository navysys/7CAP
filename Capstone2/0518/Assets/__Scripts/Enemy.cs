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
        
    }
    void Update()
	{
		if(Input.GetKeyDown(KeyCode.Keypad8))
			bar.SetHp(this.HP, this.maxHP);

	}

	public void Setup(EnemyInfo info)
	{
		this.enemyinfo = info;

		image.sprite = this.enemyinfo.image;
		maxHP = this.enemyinfo.HP;
		power = this.enemyinfo.Power;
		HP = maxHP;
	}

	public void ReduceHP(int i)
	{
		HP -= i;
		if (HP <= 0)
		{
			HP = 0;
			//animator 죽는모션 추가해야함
			GameManager.instance.enemySpawner.ReduceEnemyCount();
		}
		bar.SetHp(HP, maxHP);
	}

	public void Action()
	{
		GameManager.instance.player.GetComponent<Player>().ReduceHP(5);
	}
}
