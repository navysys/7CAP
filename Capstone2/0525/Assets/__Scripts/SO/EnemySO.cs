using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyInfo
{
	public string name;
	public int HP;
	public int Power;
	public Sprite image;
}

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Object/EnemySO")]
public class EnemySO : ScriptableObject
{
	public EnemyInfo[] Enemys;
}
