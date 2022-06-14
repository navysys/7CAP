using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject obj;

    public void EnableUI()
    {
        // ������ ������Ʈ�� Ȱ��ȭ
        obj.SetActive(true);
    }

    public void DisableUI()
    {
        // ������ ������Ʈ�� ��Ȱ��ȭ
        obj.SetActive(false);
    }

    public void DelayDisableUI()
    {
        Invoke("DisableUI", 2.0f);
    }


    public void DisableMapUI()
    {
        // ������ ������Ʈ�� ��Ȱ��ȭ
        obj.SetActive(false);
        Map.MapPlayerTracker.Instance.Locked = false;
    }
}
