using UnityEngine;
using UnityEngine.Rendering;

public sealed class DifficultyDirector : MonoBehaviour
{
    [Header("Difficulty")]
    [Tooltip("difficulty = elapsedTime * difficultyRate (example)")]
    [SerializeField] private float difficultyRate = 0.1f;

    [Header("Spawn Interval Formula")]
    [Tooltip("spawnInterval = baseSpawnInterval - elapsedTime * spawnIntervalDecreaseK")]
    [SerializeField] private float baseSpawnInterval = 2.0f;
    [SerializeField] private float spawnIntervalDecreaseK = 0.02f;
    [SerializeField] private float minSpawnInterval = 0.35f;

    [Header("Speed Formula")]
    [Tooltip("speed = baseMoveSpeed + difficulty * speedIncreaseK")]
    [SerializeField] private float baseMoveSpeed = 3.5f;
    [SerializeField] private float speedIncreaseK = 1.0f;
    [SerializeField] private float maxMoveSpeed = 12.0f;

    // Runtime (read-only)
    [SerializeField] private float elapsedTime = 0f;
    [SerializeField] private float difficulty = 0f;

    public float currentSpawnInterval;

    public float currentMoveSpeed;

    public float ElapsedTime => elapsedTime;
    public float Difficulty => difficulty;

    private void OnTimeUpdated(float remaining, float max)
    {
        elapsedTime = Mathf.Max(0f, max - remaining);
        
        difficulty = EvaluateDifficulty(elapsedTime);

        currentSpawnInterval = ComputeSpawnInterval(difficulty);
        currentMoveSpeed = ComputeSpeed(difficulty);


    }

    public float EvaluateDifficulty(float elapsedTimeSec)
    {
        float t = Mathf.Max(0f, elapsedTimeSec);
        
        float d = t * difficultyRate;

        return d;
    }

    public float ComputeSpawnInterval(float currentDifficulty)
    {
        float d = Mathf.Max(0f,currentDifficulty);

        float interval = baseSpawnInterval - d * spawnIntervalDecreaseK;

        interval = Mathf.Max(interval, minSpawnInterval);

        return interval;
    }

    public float ComputeSpeed(float currentDifficulty)
    {
        float d = Mathf.Max(0f, currentDifficulty);

        float speed = baseMoveSpeed + d * speedIncreaseK;

        return speed;
    }

}
