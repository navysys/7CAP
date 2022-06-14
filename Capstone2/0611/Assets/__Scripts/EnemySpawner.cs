using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] GameObject EnemyPrefab;
	[SerializeField] EnemySO enemySO;
	[SerializeField] EnemyInfo enemyinfo;
	

	public List<Enemy> enemyList;

	Transform EnemyPos;
	public EnemyArray MinorArray;
	public EnemyArray EliteArray;
	public EnemyArray BossArray;
	public List<Vector3> List_Pos;

	float EnemyDistance = 3f;

	int EnemyNum;		//스테이지 최대 적유닛 갯수
	int EnemyCount;		//현재 적유닛 갯수

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

	public void EliteEnemySpawn()
	{

		int ranint = Random.Range(0, MinorArray.EArrays.Length);
		SetEnemyPosList(EliteArray.EArrays[ranint].ENum.Length);

		for (int i = 0; i < EliteArray.EArrays[ranint].ENum.Length; i++)
		{
			Spawn(EliteArray.EArrays[ranint].ENum[i]);
		}
	}

	public void BossEnemySpawn()
	{

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
			for (int i = 0; i < EnemyC; i++)
			{
				if (i % 2 == 0)
				{
					Trans = new Vector3(EPos.x - (EnemyDistance / 2) + Mathf.Floor(i / 2) * EnemyDistance, EPos.y, EPos.z);
					List_Pos.Add(Trans);
				}
				else
				{
					Trans = new Vector3(EPos.x + (EnemyDistance / 2) - Mathf.Floor(i / 2) * EnemyDistance, EPos.y, EPos.z);
					List_Pos.Add(Trans);
				}
				Trans = EnemyPos.position;
			}

		}
		else if (EnemyC % 2 == 1)
		{
			if (EnemyC == 1)
			{
				List_Pos.Add(EPos);
			}
			else if (EnemyC == 3)
			{
				EPos = new Vector3(EPos.x - EnemyDistance, EPos.y, EPos.z);
				List_Pos.Add(EPos);
				EPos = Trans;
				EPos = new Vector3(EPos.x, EPos.y, EPos.z);
				List_Pos.Add(EPos);
				EPos = new Vector3(EPos.x + EnemyDistance, EPos.y, EPos.z);
				List_Pos.Add(EPos);
				EPos = Trans;
			}
			else
			{
				EPos = new Vector3(EPos.x - EnemyDistance * 2, EPos.y, EPos.z);
				List_Pos.Add(EPos);
				EPos = Trans;
				EPos = new Vector3(EPos.x - EnemyDistance, EPos.y, EPos.z);
				List_Pos.Add(EPos);
				EPos = Trans;
				EPos = new Vector3(EPos.x, EPos.y, EPos.z);
				List_Pos.Add(EPos);
				EPos = new Vector3(EPos.x + EnemyDistance, EPos.y, EPos.z);
				List_Pos.Add(EPos);
				EPos = Trans;
				EPos = new Vector3(EPos.x + EnemyDistance * 2, EPos.y, EPos.z);
				List_Pos.Add(EPos);
				EPos = Trans;
			}

			/*			for (float i = 0; i < EnemyC; i++)
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
			}*/
		}
		else
		{
			Debug.Log("EnemyNum이 0, 정렬할 Enemy가 없음 ");
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
		//보상창 나오고 카드 3개 보여줌 -> 덱에 추가
		//현재는 그냥 맵으로 이동
		if (EnemyCount == 0)
		{
			ClearEnemy();
			GameManager.instance.sceneDirector.SceneChange("GameMap");
		}
	}

	public void EnemyAction()
	{

		StartCoroutine(ActionDelay());
	}

	IEnumerator ActionDelay()
	{
		for (int i = 0; i < EnemyCount; i++)
		{
			enemyList[i].Action();
			yield return new WaitForSeconds(0.5f);
		}
	}

	void OnEnable()
	{
		// 델리게이트 체인 추가
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
			EliteEnemySpawn();
		}
		if (scene.name == "Boss")
		{
			BossEnemySpawn();
		}	
	}

	void OnDisable()
	{
		// 델리게이트 체인 제거
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
}
