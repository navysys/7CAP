using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyArrayInfo
{
	public int[] ENum;
}

[CreateAssetMenu(fileName = "EnemyArraySO", menuName = "Scriptable Object/EnemyArraySO")]
public class EnemyArray : ScriptableObject
{
	public EnemyArrayInfo[] EArrays;
}