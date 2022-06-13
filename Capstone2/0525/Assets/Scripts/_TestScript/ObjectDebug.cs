using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDebug : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("transform.position : " + gameObject.transform.position + "\n");
        //Debug.Log("rectTransform.position : " + gameObject.rectTransform.position + "\n");
        Debug.Log(".GetComponent<RectTransform>().position : " + gameObject.GetComponent<RectTransform>().position + "\n");
        Debug.Log(".GetComponent<RectTransform>().anchoredPosition : " + gameObject.GetComponent<RectTransform>().anchoredPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
