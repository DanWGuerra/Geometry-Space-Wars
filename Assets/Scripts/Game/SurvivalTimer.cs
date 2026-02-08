using UnityEngine;
using System;

public class SurvivalTime : MonoBehaviour
{
    public float ElapsedTime { get; private set; }
    public bool IsRunning { get; private set; }

    public event Action<float> OnTimeUpdated;

    private void Start()
    {
        StartTimer();
    }

    private void Update()
    {
        if (!IsRunning)
            return;

        ElapsedTime += Time.deltaTime;
        OnTimeUpdated?.Invoke(ElapsedTime);
    }

    public void StopTimer()
    {
        IsRunning = false;
    }

    private void StartTimer()
    {
        ElapsedTime = 0f;
        IsRunning = true;
    }
}
