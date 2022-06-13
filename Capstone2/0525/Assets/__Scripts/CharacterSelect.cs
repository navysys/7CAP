using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameObject characterSelect;

    static float ScrollSpeed = -1500f;

    void Update()
    {
        //Debug.Log("transform.position : " + characterSelect.transform.position + "\n");
        //Debug.Log("GetComponent<RectTransform>().anchoredPosition : " + characterSelect.GetComponent<RectTransform>().anchoredPosition);

        if (characterSelect.activeSelf && characterSelect.GetComponent<RectTransform>().anchoredPosition.y >= 0)
                characterSelect.transform.Translate(0, Time.deltaTime * ScrollSpeed, 0);
    }
}
