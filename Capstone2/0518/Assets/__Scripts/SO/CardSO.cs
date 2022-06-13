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
	public Sprite cardBack;
	public Sprite violet;
	public Sprite icon;
	public Sprite oval;
	public Sprite image;
	public int price;
}

[CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Object/CardSO")]
public class CardSO : ScriptableObject
{
	public CardInfo[] cards;
}
