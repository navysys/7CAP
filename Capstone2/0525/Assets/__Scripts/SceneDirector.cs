using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Scene 전환을 위한 스크립트
public class SceneDirector : MonoBehaviour
{
    public static SceneDirector instance;

    GameManager gm;
    UIManager uiManager;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

	private void Start()
	{
        uiManager = GameManager.instance.uiManager;
    }

	public void SceneChange(string scenename)
	{
        uiManager.FadeOut();    //페이드 아웃
        switch (scenename)
		{
            case "Title":
                SceneManager.LoadScene("Title");
                break;
            case "HowToPlay":
                break;
            case "GameOver":
                break ;
            case "MinorEnemy":
                StartCoroutine(WaitForChangeScene(scenename, 0.1f));
                break;
            case "EliteEnemy":
                StartCoroutine(WaitForChangeScene(scenename, 0.1f));
                break;
            case "Boss":
                StartCoroutine(WaitForChangeScene(scenename, 0.1f));
                break;
            case "GameMap":
                StartCoroutine(WaitForChangeScene(scenename, 2.0f));
                break;
            case "Store":
                StartCoroutine(WaitForChangeScene(scenename, 0.1f));
                break;
            default:
                Debug.Log("씬교체 : 디폴트(미지정인수)");
                break;
		}
	}

    // n초 뒤에 전투 씬으로 넘어감
    IEnumerator WaitForChangeScene(string scenename, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(scenename);
    }

}
