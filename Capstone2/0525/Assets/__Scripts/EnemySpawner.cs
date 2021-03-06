using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] GameObject EnemyPrefab;
	[SerializeField] EnemySO enemySO;
	[SerializeField] EnemyInfo enemyinfo;
	

	[SerializeField] List<Enemy> enemyList;

	Transform EnemyPos;
	public EnemyArray MinorArray;
	public List<Vector3> List_Pos;

	float EnemyDistance = 3f;

	int EnemyNum;		//???????? ?ִ? ?????? ????
	int EnemyCount;		//???? ?????? ????

	private void Start()
	{
		List_Pos = new List<Vector3>();
		enemyList = new List<Enemy>();
		enemyinfo = enemySO.Enemys[0];

		EnemyPos = transform;
		EnemyNum = 3;
		EnemyCount = 0;

		SetEnemyPosList(EnemyNum);
	}

	public void Spawn(int num)
	{
		enemyinfo = enemySO.Enemys[num];
		var enemyobj = Instantiate(EnemyPrefab, List_Pos[EnemyCount], Quaternion.identity);
		enemyobj.GetComponent<Enemy>().Setup(enemyinfo);
		enemyList.Add(enemyobj.GetComponent<Enemy>());
		EnemyCount++;
	}

	public void MinorEnemySpawn()
	{
		
		int ranint = Random.Range(0, MinorArray.EArrays.Length);
		SetEnemyPosList(MinorArray.EArrays[ranint].ENum.Length);

		for (int i = 0; i < MinorArray.EArrays[ranint].ENum.Length; i++)
		{
			Spawn(MinorArray.EArrays[ranint].ENum[i]);
		}
	}

	void ClearEnemy()
	{
		List_Pos.Clear();
		enemyList.Clear();
	}

	public void SetEnemyPosList(int EnemyC)
	{
		List_Pos.Clear();
		Vector3 Trans = EnemyPos.position;
		Vector3 EPos = Trans;

		if (EnemyC % 2 == 0 && EnemyC != 0)
		{
			for(int i =0; i < EnemyC; i++)
			{
				if(i % 2 == 0)
				{
					Trans = new Vector3(EPos.x - (EnemyDistance / 2) - Mathf.Floor(i / 2) * EnemyDistance, EPos.y, EPos.z);
					List_Pos.Add(Trans);
				}
				else
				{
					Trans = new Vector3(EPos.x + (EnemyDistance / 2) + Mathf.Floor(i / 2) * EnemyDistance, EPos.y, EPos.z);
					List_Pos.Add(Trans);
				}
				Trans = EnemyPos.position;
			}
			
		}
		else if (EnemyC % 2 == 1)
		{
			for (float i = 0; i < EnemyC; i++)
			{
				if (i % 2 == 0)
				{
					Trans = new Vector3(EPos.x + EnemyDistance * Mathf.Floor(i / 2), EPos.y, EPos.z);
					List_Pos.Add(Trans);
				}
				else
				{
					Trans = new Vector3(EPos.x - EnemyDistance * Mathf.Ceil(i / 2), EPos.y, EPos.z);
					List_Pos.Add(Trans);
				}
				Trans = EnemyPos.position;
			}
		}
		else
		{
			Debug.Log("EnemyNum?? 0, ?????? Enemy?? ???? ");
		}
	}

	public void SortingPos()
	{
		for(int i = 0; i < EnemyCount; i++)
		{
			enemyList[i].transform.position = List_Pos[i];
		}
	}

	public void ReduceEnemyCount(Enemy e)
	{
		enemyList.Remove(e);
		--EnemyCount;
		SetEnemyPosList(EnemyCount);
		SortingPos();
		//????â ?????? ī?? 3?? ?????? -> ???? ?߰?
		//?????? ?׳? ?????? ?̵?
		if (EnemyCount == 0)
		{
			ClearEnemy();
			GameManager.instance.sceneDirector.SceneChange("GameMap");
		}
	}

	public void EnemyAction()
	{
		for (int i = 0; i < EnemyCount; i++)
		{
			enemyList[i].Action();
		}
	}

	void OnEnable()
	{
		// ????????Ʈ ü?? ?߰?
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.name == "MinorEnemy")
		{
			MinorEnemySpawn();
		}
		if(scene.name == "EliteEnemy")
		{

		}
		if (scene.name == "Boss")
		{

		}	
	}

	void OnDisable()
	{
		// ????????Ʈ ü?? ????
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
}
