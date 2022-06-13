using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] GameObject EnemyPrefab;
	[SerializeField] EnemySO enemySO;
	[SerializeField] EnemyInfo enemyinfo;
	[SerializeField] Enemy[] enemyArr;

	Transform EnemyPos;
	public List<Vector3> List_Pos;

	float EnemyDistance = 3f;

	int EnemyNum;		//스테이지 최대 적유닛 갯수
	int EnemyCount;		//현재 적유닛 갯수

	private void Start()
	{
		List_Pos = new List<Vector3>();
		enemyArr = new Enemy[5];
		enemyinfo = enemySO.Enemys[0];

		EnemyPos = transform;
		EnemyNum = 3;
		EnemyCount = 0;

		SetEnemyPosList(EnemyNum);
	}

	public void Spawn()
	{
		for(int i = 0; i < EnemyNum; i++)
		{
			int ran = Random.Range(0, enemySO.Enemys.Length);
			enemyinfo = enemySO.Enemys[ran];
			var enemyobj = Instantiate(EnemyPrefab, List_Pos[i], Quaternion.identity);
			enemyobj.GetComponent<Enemy>().Setup(enemyinfo);
			enemyArr[i] = enemyobj.GetComponent<Enemy>();
			EnemyCount++;
		}
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
			Debug.Log("EnemyNum이 0, 정렬할 Enemy가 없음 ");
		}
	}

	public void SortingPos()
	{
		for(int i = 0; i < List_Pos.Count; i++)
		{
			enemyArr[i].transform.position = List_Pos[i];
		}
	}

	public void ReduceEnemyCount()
	{
		EnemyCount--;
		//보상창 나오고 카드 3개 보여줌 -> 덱에 추가
		//현재는 그냥 맵으로 이동
		if(EnemyCount == 0)
		{
			GameManager.instance.sceneDirector.SceneChange("GameMap");
		}
	}

	public void EnemyAction()
	{
		for (int i = 0; i < EnemyCount; i++)
		{
			enemyArr[i].Action();
		}
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Keypad7))
		{
			EnemyAction();
		}
	}
}
