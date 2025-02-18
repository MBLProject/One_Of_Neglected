using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    [Header("시간 설정")]
    private float gameTime = 0f;
    public float GameTime => gameTime;

    [Header("디버그 설정")]
    [SerializeField] private bool isDebugMode = false;
    [SerializeField, Range(0f, 660f)] private float debugTime = 0f;

    // 마지막 이벤트 발생 시간 저장
    private float lastThirtySecEvent = -30f;  // 30초 이벤트
    private float lastOneMinEvent = -60f;     // 1분 이벤트
    private float lastOneMinThirtyEvent = -90f;  // 1분 30초 이벤트
    private float lastOneMinFiftyEvent = -110f;  // 1분 50초 이벤트


    public event Action OnThirtySecondsPassed;  
    public event Action OnMinutePassed;
    public event Action OnOneMinThirtySecondsPassed;  
    public event Action OnOneMinFiftySecondsPassed;   


    private void Update()
    {
        if (GameManager.Instance.isPaused || !GameManager.Instance.isGameStarted) return;

        UpdateGameTime();
        CheckTimeBasedEvents();
    }

    private void UpdateGameTime()
    {
        if (isDebugMode)
        {
            gameTime = debugTime;
            Debug.Log($"[Debug] Game Time: {GetFormattedTime()} ({gameTime:F1}초)");
        }
        else
        {
            gameTime += Time.deltaTime;
        }
    }

    private void CheckTimeBasedEvents()
    {
        // 30초 이벤트 체크
        if (gameTime >= lastThirtySecEvent + 30f)
        {
            lastThirtySecEvent = Mathf.Floor(gameTime / 30f) * 30f;
            OnThirtySecondsPassed?.Invoke();
        }

        // 1분 이벤트 체크
        if (gameTime >= lastOneMinEvent + 60f)
        {
            lastOneMinEvent = Mathf.Floor(gameTime / 60f) * 60f;
            OnMinutePassed?.Invoke();
        }
        // 1분 30초 이벤트 체크 (90초)
        if (gameTime >= lastOneMinThirtyEvent + 90f)
        {
            lastOneMinThirtyEvent = Mathf.Floor(gameTime / 90f) * 90f;
            OnOneMinThirtySecondsPassed?.Invoke();
        }

        // 1분 50초 이벤트 체크 (110초)
        if (gameTime >= lastOneMinFiftyEvent + 110f)
        {
            lastOneMinFiftyEvent = Mathf.Floor(gameTime / 110f) * 110f;
            OnOneMinFiftySecondsPassed?.Invoke();
        }
    }


    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(gameTime / 60f);
        int seconds = Mathf.FloorToInt(gameTime % 60f);
        return $"{minutes:D2}:{seconds:D2}";
    }

    public void ResetTime()
    {
        gameTime = 0f;
        lastThirtySecEvent = -30f;
        lastOneMinEvent = -60f;
        lastOneMinThirtyEvent = -90f;
        lastOneMinFiftyEvent = -110f;
    }

    public void SetDebugTime(float time)
    {
        if (!isDebugMode) return;
        debugTime = Mathf.Clamp(time, 0f, 660f);
    }
}