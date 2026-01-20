using UnityEngine;

[CreateAssetMenu(
    fileName = "MonsterDef_",
    menuName = "Game/Monster/Monster Definition"
)]
public class MonsterDef : ScriptableObject
{
    [Header("Identity")]
    public string monsterId;
    public string displayName;

    [Header("Prefab")]
    public GameObject prefab;

    [Header("Core Stats")]
    public float maxHp;
    public float moveSpeed;
    public float damage;
}
