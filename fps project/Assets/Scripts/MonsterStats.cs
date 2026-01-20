using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    public float CurrentHp { get; private set; }
    public float MoveSpeed { get; private set; }
    public float Damage { get; private set; }

    public void InitFromDef(float hp, float damage, float moveSpeed)
    {
        CurrentHp = hp;
        MoveSpeed = moveSpeed;
        Damage = damage;
    }

    public void ApplySpeedMultiplier(float multiplier)
    {
         MoveSpeed *= multiplier;
    }
}
