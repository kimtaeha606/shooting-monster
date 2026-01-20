using Mono.Cecil.Cil;
using UnityEngine;


public sealed class MonsterSpawner : MonoBehaviour
{
   
    [Header("Spawn Points (optional)")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Parent (optional)")]
    [SerializeField] private Transform spawnRoot;

    [Header("Ref")]
    [SerializeField] private DifficultyDirector director;

    [Header("Spawn Control")]
    [SerializeField] private MonsterDef monsterDef;
    [SerializeField] private bool autoSpawn = true;

    private float nextSpawnTime;   

    

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

        if (director == null) return;

        director.elapsedTime = Mathf.Max(0f, elapsed);

        director.difficulty = director.EvaluateDifficulty(director.elapsedTime);
        director.currentSpawnInterval = director.ComputeSpawnInterval(director.difficulty);
        director.currentMoveSpeed = director.ComputeSpeed(director.difficulty);
    }

    private void Update()
    {
        if (!autoSpawn) return;
        if (monsterDef == null) return;

        float interval = 1f;
        if (director != null)
            interval = Mathf.Max(0.05f,director.currentSpawnInterval);
        if(Time.time < nextSpawnTime) return;

        Spawn(monsterDef);

        nextSpawnTime = Time.time + interval;
    }

    public GameObject Spawn(MonsterDef def)
    {
        if ( def == null)
        {
#if UNITY_EDITOR
            Debug.LogError("[MonsterSpawner] monsterPrefab is null.");
#endif
            return null;
        }

        float hp = def.maxHp;
        float dmg = def.damage;
        float spd = def.moveSpeed * director.currentMoveSpeed;

        Transform parent = spawnRoot != null ? spawnRoot : transform;
        
        Vector3 pos;
        Quaternion rot;

        if (spawnPoints != null && spawnPoints.Length > 0)
        {
            Transform p = spawnPoints[Random.Range(0,spawnPoints.Length)];
            pos = p.position;
            rot = p.rotation;
        }
        else
        {
            pos = parent.position;
            rot = parent.rotation;
        }

        GameObject monster = Instantiate(
            def.prefab,
            pos,
            rot,
            parent
        );

        

        var stats = monster.GetComponent<MonsterStats>();
        stats.InitFromDef(hp,dmg,spd);
        
        
        return monster;
    } 
}
