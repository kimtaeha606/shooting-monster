using UnityEngine;

public sealed class MonsterSpawner : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject monsterPrefab;
    
    [Header("Spawn Points (optional)")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Parent (optional)")]
    [SerializeField] private Transform spawnRoot;

    public GameObject Spawn(MonsterDef def)
    {
        if (monsterPrefab == null)
        {
#if UNITY_EDITOR
            Debug.LogError("[MonsterSpawner] monsterPrefab is null.");
#endif
            return null;
        }

        Transform parent = spawnRoot != null ? spawnRoot : transform;
        
        Vector3 pos;
        Quaternion rot;

        if (spawnPoints != null && spawnPoints.Length > 0)
        {
            Transform p = spawnPoints[Random.Range(0,spawnPoints.Length)];
            pos = parent.position;
            rot = parent.rotation;
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
            spawnRoot
        );
        var initiallizer = monster.GetComponent<MonsterInitializer>();
        initiallizer?.Initialize(def);

        return monster;
    } 
}
