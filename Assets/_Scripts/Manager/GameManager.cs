using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isPaused = false;
    private bool isGameStarted = false;  // 게임 시작 상태 체크

    private void Update()
    {
        // 스페이스바로 게임 시작
        if (Input.GetKeyDown(KeyCode.Space) && !isGameStarted)
        {
            Debug.Log("Game Started by Space key!");
            isGameStarted = true;
            StartGame();
        }

        // ESC로 게임 일시정지/재개
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

        // R키로 게임 재시작
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Game Restarted by R key!");
            StartGame();
        }
    }

    public void StartGame()
    {
        Debug.Log("GameManager: StartGame called");
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
}
