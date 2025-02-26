using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isPaused = false;
    public bool isGameStarted = false;
    public float gold;
    public int remnents;
    private void Start()
    {
        StartGame();

    }
    private void Update()
    {
        HandleInputs();
    }
    private void HandleInputs()
    {
        // ESC로 일시정지/재개
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                UI_Manager.Instance.panel_Dic["Option_Panel"].PanelClose(true);
                Time.timeScale = 1;
                isPaused = false;
            }
            else
            {
                UI_Manager.Instance.panel_Dic["Option_Panel"].PanelOpen();
                Time.timeScale = 0;
                isPaused = true;
            }
        }

    }

    public void StartGame()
    {
        isPaused = false;
        isGameStarted = true;

        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.ResetTime();
        }
        else
        {
            Debug.LogError("TimeManager is not initialized!");
        }

        if (UnitManager.Instance != null)
        {
            UnitManager.Instance.StartGame();
        }
        else
        {
            Debug.LogError("UnitManager is not initialized!");
        }
    }
}