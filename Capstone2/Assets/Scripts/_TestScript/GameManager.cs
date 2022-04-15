using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤을 할당할 전역 변수

    public bool isGameStart = false; // 게임 스타트 상태
    public bool isPause = false; // 일시정지 상태
    public bool isGameOver = false; // 게임 오버 상태

    // 싱글톤
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Scene에 두 개 이상의 GameManager가 존재합니다!");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        // 게임 시작이 아니면 실행 종료
        if (!isGameStart)
        {
            return;
        }

        // 게임 오버 상태이면 실행 종료
        if (isGameOver)
        {
            return;
        }
    }

}