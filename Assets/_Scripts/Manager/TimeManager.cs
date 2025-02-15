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
    [SerializeField, Range(0f, 600f)] private float debugTime = 0f;

    // 마지막 이벤트 발생 시간 저장
    private float lastThirtySecEvent = -30f;  // 30초 이벤트
    private float lastOneMinEvent = -60f;     // 1분 이벤트
    private int lastMinute = -1;              // 분 변경 체크

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
            OnThirtySecondsInterval();
        }

        // 1분 이벤트 체크
        if (gameTime >= lastOneMinEvent + 60f)
        {
            int currentMinute = Mathf.FloorToInt(gameTime / 60f);
            lastOneMinEvent = Mathf.Floor(gameTime / 60f) * 60f;
            OnMinuteInterval(currentMinute);
        }
    }

    private void OnThirtySecondsInterval()
    {
        Debug.Log($"30초 이벤트 발생! 현재 시간: {GetFormattedTime()}");
        // 여기에 30초마다 실행할 이벤트 추가
    }

    private void OnMinuteInterval(int minute)
    {
        Debug.Log($"분 이벤트 발생! {minute}분 경과");

        // 여기에 분마다 실행할 이벤트 추가
        switch (minute)
        {
            case 1:
                // 1분 경과 시 이벤트
                break;
            case 2:
                // 2분 경과 시 이벤트
                break;
            case 3:
                // 3분 경과 시 이벤트
                break;
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
        lastMinute = -1;
    }

    public void SetDebugTime(float time)
    {
        if (!isDebugMode) return;
        debugTime = Mathf.Clamp(time, 0f, 600f);
    }
}