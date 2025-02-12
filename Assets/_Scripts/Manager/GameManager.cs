using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isPaused = false;
    private bool isGameStarted = false;  // 寃뚯엫 ?쒖옉 ?곹깭 泥댄겕

    private void Update()
    {
        // ?ㅽ럹?댁뒪諛붾줈 寃뚯엫 ?쒖옉
        if (Input.GetKeyDown(KeyCode.Space) && !isGameStarted)
        {
            Debug.Log("Game Started by Space key!");
            isGameStarted = true;
            StartGame();
        }

        // ESC濡?寃뚯엫 ?쇱떆?뺤?/?ш컻
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

        // R?ㅻ줈 寃뚯엫 ?ъ떆??
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
