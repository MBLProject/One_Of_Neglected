using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isPaused = false;
    public bool isGameStarted = false;

    private void Update()
    {
        HandleInputs();
    }

    private void HandleInputs()
    {
        // 스페이스바로 게임 시작
        if (Input.GetKeyDown(KeyCode.Space) && !isGameStarted)
        {
            Debug.Log("Game Started by Space key!");
            StartGame();
        }

        // ESC로 일시정지/재개
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

        if (UnitManager.Instance != null)
        {
            UnitManager.Instance.StartGame();
        }
        else
        {
            Debug.LogError("UnitManager is not initialized!");
        }
    }

    public void PauseGame()
    {
        Debug.Log("GameManager: PauseGame called");
        isPaused = true;

        if (UnitManager.Instance != null)
        {
            UnitManager.Instance.PauseGame();
        }
    }

    public void ResumeGame()
    {
        Debug.Log("GameManager: ResumeGame called");
        isPaused = false;

        if (UnitManager.Instance != null)
        {
            UnitManager.Instance.ResumeGame();
        }
    }
}