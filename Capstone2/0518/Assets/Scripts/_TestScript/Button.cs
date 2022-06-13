using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject obj;

    public void EnableUI()
    {
        // 선택한 오브젝트를 활성화
        obj.SetActive(true);
    }

    public void DisableUI()
    {
        // 선택한 오브젝트를 비활성화
        obj.SetActive(false);
    }

    public void OpenDeckList()
    {

    }

    public void GameMapCloseUI()
    {
        // 선택한 오브젝트를 비활성화
        obj.SetActive(false);
        Map.MapPlayerTracker.Instance.Locked = false;
    }
}
