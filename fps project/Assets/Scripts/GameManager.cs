using System.Runtime.CompilerServices;
using Unity.FPS.Game;
using UnityEngine;

public class GameManager : MonoBehaviour


{
    [SerializeField] private Health health;
    [SerializeField] private ScoreManager score;

    public float FinalScore;

    private void OnEnable()
    { 
        health.OnDie += SetGameOver;
    
    }

    private void OnDisable()
    {
        health.OnDie -= SetGameOver;
    }

    private void SetGameOver()
    {
        FinalScore = score.GetSurvivalTime();
        score.OnGameOver(FinalScore);
    }
}
