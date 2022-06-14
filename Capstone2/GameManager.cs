using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Relic[] RelicList;

    public GameObject Player;
    public GameObject Enemy;

    enum GameState { Start, Main, Reward, End };
    GameState State = GameState.Start;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
