using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameObject characterSelect;

    public void UICharacterSelect()
    {
        // ĳ���� ���� ������Ʈ�� Ȱ��ȭ
        // ĳ���͸� �����ϸ� ���� �� ������ �Ѿ
        characterSelect.SetActive(true);
    }
}
