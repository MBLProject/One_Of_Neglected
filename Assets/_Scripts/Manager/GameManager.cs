using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isPaused = false;
    private bool isGameStarted = false;  // ?濡ろ뜐?????筌믨퀣援????ㅺ컼??癲ル슪???띿물?

    private float gameTime = 0f;  
    public float GameTime => gameTime;


    [Header("디버그 설정")]
    [SerializeField] private bool isDebugMode = false;
    [SerializeField, Range(0f, 600f)] private float debugGameTime = 0f;

    private void Update()
    {
        if (isDebugMode)
        {
            // 디버그 모드일 때는 Inspector에서 설정한 시간 사용
            gameTime = debugGameTime;
        }
        else
        {
            // 일반 모드에서는 정상적으로 시간 증가
            gameTime += Time.deltaTime;
        }
        if (isDebugMode)
        {
            Debug.Log($"Game Time: {GetFormattedTime()} ({gameTime:F1}초)");
        }

        // ????됱뱻???⑤８痢?袁⑸즴??繞??濡ろ뜐?????筌믨퀣援?
        if (Input.GetKeyDown(KeyCode.Space) && !isGameStarted)
        {
            Debug.Log("Game Started by Space key!");
            isGameStarted = true;
            StartGame();
        }

        // ESC???濡ろ뜐?????繹먮굝六?嶺?/????
        if (Input.GetKeyDown(KeyCode.Escape) && isGameStarted)
        {
            if (isPaused)
            {
                Debug.Log("Game Resumed by ESC key!");
                ResumeGame();
            }
            else
            {
                Debug.Log("Game Paused by ESC key!");
                PauseGame();
            }
        }

        // R????겾??濡ろ뜐????????
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Game Restarted by R key!");
            StartGame();
        }
    }

    public void StartGame()
    {
        Debug.Log("GameManager: StartGame called");
        gameTime = 0f;
        isPaused = false;
        isGameStarted = true;
        UnitManager.Instance.StartGame();
    }

    public void PauseGame()
    {
        Debug.Log("GameManager: PauseGame called");
        isPaused = true;
        UnitManager.Instance.PauseGame();
    }

    public void ResumeGame()
    {
        Debug.Log("GameManager: ResumeGame called");
        isPaused = false;
        UnitManager.Instance.ResumeGame();
    }
    // 현재 게임 시간을 분:초 형식의 문자열로 반환
    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(gameTime / 60f);
        int seconds = Mathf.FloorToInt(gameTime % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    // 다른 스크립트에서 특정 시간 간격을 체크할 때 사용할 수 있는 메서드
    public bool IsTimeInterval(float interval)
    {
        return Mathf.Floor(gameTime % interval) == 0;
    }
}
