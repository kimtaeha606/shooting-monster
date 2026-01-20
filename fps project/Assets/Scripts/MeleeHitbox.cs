using UnityEngine;
using Unity.FPS.Game;
public class MeleeHitbox : MonoBehaviour
{
    [SerializeField] private MonsterAttack owner;

    void OnTriggerEnter(Collider other)
    {
        Damageable damageable = other.GetComponent<Damageable>();
        if (!damageable) return;

        owner.TryHit(damageable);
    }
}
