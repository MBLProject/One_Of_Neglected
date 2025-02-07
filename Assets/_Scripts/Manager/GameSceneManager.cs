using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static string nextScene;
    private void Start()
    {
        StartCoroutine(LoadSceneCoroutine());
    }
    public static void SceneLoad(string sceneName)
    {
        nextScene = sceneName;
        Debug.Log($"다음 씬 : {nextScene}");
        SceneManager.LoadScene("LoadingScene");

    }
    private IEnumerator LoadSceneCoroutine()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        while (!op.isDone)
        {
            Debug.Log("씬로드중");
            if (op.progress >= 0.9f)
            {
                Debug.Log("90퍼 이상 로드완료");
                op.allowSceneActivation = true;
                yield break;
            }
        }
        Debug.Log("루틴 종료");
    }
}
