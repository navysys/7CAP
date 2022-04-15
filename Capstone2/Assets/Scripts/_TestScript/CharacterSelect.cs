using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameObject characterSelect;

    public void UICharacterSelect()
    {
        // 캐릭터 선택 오브젝트를 활성화
        // 캐릭터를 선택하면 게임 맵 씬으로 넘어감
        characterSelect.SetActive(true);
    }
}
