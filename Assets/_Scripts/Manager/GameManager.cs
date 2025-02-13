using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isPaused = false;
    private bool isGameStarted = false;  // ?濡ろ뜐?????筌믨퀣援????ㅺ컼??癲ル슪???띿물?

    private void Update()
    {
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
