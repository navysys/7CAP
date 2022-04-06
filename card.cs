using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class card : MonoBehaviour
{
    // Start is called before the first frame update
    public int count;
    public int allcount;
    public GameObject manager;
    void Start()
    {
        manager = GameObject.Find("manager");
        count = manager.GetComponent<cardsetting>().count;
        manager.GetComponent<cardsetting>().count = count + 1;
        this.transform.DOMove(new Vector3(0, 0, 0), 1);

    }

    // Update is called once per frame
    void Update()
    {
        allcount = manager.GetComponent<cardsetting>().count;
        if(count<allcount / 2)
        this.transform.DOMove(new Vector3(count - allcount/2, (count - allcount /2)*0.2f, 0), 1);
        else
        this.transform.DOMove(new Vector3(count - allcount / 2, -(count - allcount / 2) * 0.2f, 0), 1);

        this.transform.DORotate(new Vector3(0,0,-(count - allcount / 2)*5),1);
       
    }

}
