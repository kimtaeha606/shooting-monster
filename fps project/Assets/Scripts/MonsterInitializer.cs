using UnityEngine;

public class MonsterInitializer : MonoBehaviour
{
    [SerializeField] private MonsterStats stats;

    public void Initialize(MonsterDef def)
    {
        stats.InitFromDef(def);
    }
}
