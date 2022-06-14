using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public enum eCardState
{
	store,
	nonstore
}

public class Card : MonoBehaviour
{
	[SerializeField] SpriteRenderer card_front;
	[SerializeField] SpriteRenderer card_violet;
	[SerializeField] SpriteRenderer card_icon;
	[SerializeField] SpriteRenderer card_oval;
	[SerializeField] SpriteRenderer card_image;
	[SerializeField] TMP_Text nameTMP;
	[SerializeField] TMP_Text effectTMP;
	[SerializeField] int price;
	[SerializeField] int type;
	[SerializeField] int value;
	[SerializeField] int range;
	[SerializeField] int costHP;
	[SerializeField] bool isKill;
	[SerializeField] int draw;

	public CardInfo cardInfo;
	public PRS originPRS;

	public void effect()
	{
		GetComponent<Effect>().C_Effect(type, value, range, costHP, isKill, draw);
		
	}


	public void Setup(CardInfo cardInfo)
	{
		this.cardInfo = cardInfo;

		card_front.sprite = this.cardInfo.cardFront;
		card_violet.sprite = this.cardInfo.violet;
		card_icon.sprite = this.cardInfo.icon;
		card_oval.sprite = this.cardInfo.oval;
		card_image.sprite = this.cardInfo.image;
		nameTMP.text = cardInfo.name;
		effectTMP.text = cardInfo.effectText;
		price = cardInfo.price;
		type = cardInfo.type;
		value = cardInfo.value;
		range = cardInfo.range;
		costHP = cardInfo.costHP;
		isKill = cardInfo.isKill;
		draw = cardInfo.draw;
	}

	public void MoveTransform(PRS prs, bool useDotween, float dotweenTime = 0)
	{
		if(useDotween)
		{
			transform.DOMove(prs.pos, dotweenTime);
			transform.DORotateQuaternion(prs.rot, dotweenTime);
			transform.DOScale(prs.scale, dotweenTime);
		}
		else
		{
			transform.position = prs.pos;
			transform.rotation = prs.rot;
			transform.localScale = prs.scale;
		}
	}

	private void OnMouseOver()
	{
		CardManager.instance.CardMouseOver(this);
	}

	private void OnMouseExit()
	{
		CardManager.instance.CardMouseExit(this);
	}

	private void OnMouseDown()
	{
		if (GameManager.instance.cardManager.isBattle)
			CardManager.instance.CardMouseDown();
		else
        {
			GameManager.instance.StoreCardClicked(this);
			CardManager.instance.AddCardToDeckInfo(cardInfo);
			Destroy(gameObject);
		}
	}

	private void OnMouseUp()
	{
		CardManager.instance.CardMouseUp();
	}

/*    public eCardState state = eCardState.nonstore;
    // 테스트용 카드 관련 스크립트
    public void OnMouseUpAsButton()
    {
        //print(name);
        GameManager.instance.StoreCardClicked(this);
    }*/
}
