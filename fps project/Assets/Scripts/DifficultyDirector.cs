using UnityEngine;


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
    
    [SerializeField] private float speedIncreaseK = 1.0f;
    

    // Runtime (read-only)
    [SerializeField] public float elapsedTime = 0f;
    [SerializeField] public float difficulty = 0f;

    public float currentSpawnInterval;

    public float currentMoveSpeed;

    

    private void OnEnable()
    {
        GameSignals.SurvivalTimeUpdated += OnElapsedTimeUpdated;
    }

    private void OnDisable()
    {
        GameSignals.SurvivalTimeUpdated -= OnElapsedTimeUpdated;
    }

    private void OnElapsedTimeUpdated(float elapsed)
    {
        elapsedTime = Mathf.Max(0f, elapsed);

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

        float multiplier = d * speedIncreaseK;

        return multiplier;
    }

    


}
