using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CardInfo
{
	public string name;
	[TextArea]
	public string effectText;
	public Sprite cardFront;
	public Sprite violet;
	public Sprite icon;
	public Sprite oval;
	public Sprite image;
	public int price;
	public int type;
	public int value;
	public int range;
	public int costHP;
	public bool isKill;
	public int draw;
}

[CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Object/CardSO")]
public class CardSO : ScriptableObject
{
	public CardInfo[] cards;
}
