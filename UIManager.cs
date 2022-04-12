using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    GameObject Player;
    GameObject EnemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameManager.instance.Player;
        EnemySpawner = GameManager.instance.EnemySpawner;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
