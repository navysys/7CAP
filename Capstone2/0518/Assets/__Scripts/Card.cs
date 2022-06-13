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

	public CardInfo cardInfo;
	public PRS originPRS;

	[SerializeField]
	public void effect() { }


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
		CardManager.instance.CardMouseDown();
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
