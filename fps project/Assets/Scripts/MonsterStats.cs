using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    public float CurrentHp { get; private set; }
    public float MoveSpeed { get; private set; }
    public float Damage { get; private set; }

    public void InitFromDef(MonsterDef def)
    {
        CurrentHp = def.maxHp;
        MoveSpeed = def.moveSpeed;
        Damage = def.damage;
    }

    public void ApplySpeedMultiplier(float multiplier)
    {
         MoveSpeed *= multiplier;
    }
}
