using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public int MaxHP = 90;
    public int HP = 90;
    public int Def;

    GameObject GOTextHP;

    public void findAllChildren(GameObject g)
    {
        Transform[] allChildren = g.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            //수행할 함수 작성
            if(child.name == "HP_Text")
			{
                GOTextHP = child.gameObject;
			}
        }
    }


    public void AddDef(int i)
	{
        Def += i;
	}

    public void ResetDef()
	{
        Def = 0;
	}

    public void ReduceHP(int i)
	{
        HP -= i;
        if(HP <= 0)
		{
            HP = 0;
            //animator 죽는모션 추가해야함
            GameManager.instance.GameOver();
		}
        GOTextHP.GetComponent<TextMeshProUGUI>().text = HP.ToString() + " / " + MaxHP.ToString();

    }

    // Start is called before the first frame update
    void Start()
    {
        findAllChildren(this.gameObject);
        ReduceHP(10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
