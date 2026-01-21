using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private float survivalTime;
    private bool isCounting;
    private bool isGameOver;

    public void InitTimer()
    {
        survivalTime = 0;
        isCounting = false;
        isGameOver = false;

        GameSignals.RaiseSurvivalTimeUpdated(0f);
    }

    public void StartTimer()
    {
        if (isGameOver == true)
            return;
        isCounting = true;
    }

    public void UpdateTimer()
    {
        if (isGameOver == true)
            return;
        if (isCounting == false)
            return;
        survivalTime += Time.deltaTime;
        GameSignals.RaiseSurvivalTimeUpdated(survivalTime);
    }

    public void StopTimer()
    {
        isCounting = false;
    }

    public float GetSurvivalTime()
    {
        return survivalTime;
    }

    public void OnGameOver(float fianlTime)
    {

        if (isGameOver == true)
            return;
        
        isGameOver = true;
        
        StopTimer();
        
        RunResult.LastSurvivalTime = fianlTime;

        GameSignals.RaiseGameOver(RunResult.LastSurvivalTime);

        SceneManager.LoadScene("LoseScene");
    }

    private void Start()
    {
        InitTimer();
        StartTimer();
    }

    private void Update()
    {
        UpdateTimer();
    }

}
