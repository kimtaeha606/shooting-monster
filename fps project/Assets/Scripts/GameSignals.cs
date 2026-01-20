using System;

public static class GameSignals
{
    public static event Action<float> SurvivalTimeUpdated;

    public static void RaiseSurvivalTimeUpdated(float time)
    {
        SurvivalTimeUpdated?.Invoke(time);
    }

    public static event Action<float> GameOver;

    public static void RaiseGameOver(float finalSurvivalTime)
    {
        GameOver?.Invoke(finalSurvivalTime);
    }
}

