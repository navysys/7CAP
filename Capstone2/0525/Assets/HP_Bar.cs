using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HP_Bar : MonoBehaviour
{
    TMP_Text HP_TMP;
    Transform Bar;
    float Bar_Scale;

	private void Awake()
	{
        findAllChildren(this.gameObject);
    }

	public void findAllChildren(GameObject g)
    {
        Transform[] allChildren = g.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            //수행할 함수 작성
            if (child.name == "HP_Text")
            {
                HP_TMP = child.GetComponent<TMP_Text>();
            }
            if(child.name == "hp_bar")
			{
                Bar = child.GetComponent<Transform>();
			}
        }
    }

    public void SetHp(int hp, int maxhp)
	{
        HP_TMP.text = hp.ToString() + " / " + maxhp.ToString();
        Bar.localScale = new Vector3((float)hp/(float)maxhp, 1, 1);
    }
}
