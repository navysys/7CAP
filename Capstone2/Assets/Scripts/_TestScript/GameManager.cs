using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // �̱����� �Ҵ��� ���� ����

    public bool isGameStart = false; // ���� ��ŸƮ ����
    public bool isPause = false; // �Ͻ����� ����
    public bool isGameOver = false; // ���� ���� ����

    // �̱���
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Scene�� �� �� �̻��� GameManager�� �����մϴ�!");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        // ���� ������ �ƴϸ� ���� ����
        if (!isGameStart)
        {
            return;
        }

        // ���� ���� �����̸� ���� ����
        if (isGameOver)
        {
            return;
        }
    }

}