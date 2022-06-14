using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FindCamera : MonoBehaviour
{
    Canvas canvas;

	private void Start()
	{
		canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
	}

	void OnEnable()
    {
        // 델리게이트 체인 추가
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != "Title")
            canvas.worldCamera = Camera.main;
    }

    void OnDisable()
    {
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
