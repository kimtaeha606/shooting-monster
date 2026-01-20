using UnityEngine;
using Unity.FPS.Game;
using System.Collections.Generic;


public class MonsterAttack : MonoBehaviour
{
    [Header("Melee Attack")]
    [SerializeField] private Collider attackCollider;
    [SerializeField] private float damage = 20f;

    private HashSet<Damageable> hitTargets = new HashSet<Damageable>();

    void Awake()
    {
        attackCollider.enabled = false;
    }

    public void EnableAttack()
    {
        hitTargets.Clear();
        attackCollider.enabled = true;
    }

    public void DisableAttack()
    {
        attackCollider.enabled = false;
    }

    public void TryHit(Damageable target)
    {
        if (hitTargets.Contains(target)) return;

        hitTargets.Add(target);
        target.InflictDamage(damage, false, gameObject);
    }

}
