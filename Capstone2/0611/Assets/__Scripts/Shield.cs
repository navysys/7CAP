using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shield : MonoBehaviour
{
    TMP_Text S_Text;

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
            if (child.name == "S_Text")
            {
                S_Text = child.GetComponent<TMP_Text>();
            }
        }
    }

    public void SetShield(int S)
    {
        S_Text.text = S.ToString();
    }
}

