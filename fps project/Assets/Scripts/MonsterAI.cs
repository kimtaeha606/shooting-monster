using NUnit.Framework;
using UnityEngine;

public sealed class MonsterAI : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private Transform target;

    [Header("Movement (Physics)")]
    [SerializeField] private Rigidbody rb;          // 3D 기준. 2D면 Rigidbody2D로 교체.
    [SerializeField] private float moveSpeed = 5f;  // TODO: 적절한 기본값 튜닝
    [SerializeField] private float stopDistance = 1.2f;

    [Header("Runtime (Read Only)")]
    [SerializeField] private Vector3 moveDir = Vector3.zero;
    [SerializeField] private bool hasTarget = false;

    private bool AcquireTarget()
    {
        if (hasTarget && target != null && target.gameObject.activeInHierarchy)
        return true;

        target = null;
        hasTarget = false;

        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        
        if (playerObj == null || !playerObj.activeInHierarchy)
        return false;

        target = playerObj.transform;
        hasTarget = true;
        return true;

    }

    private void UpdateMoveDirection()
    {
        if (target == null || target.gameObject.activeInHierarchy)
        {
            moveDir = Vector3.zero;
            return;
        }

        Vector3 dir = target.position - transform.position;

        dir.y = 0f;

        if (dir.sqrMagnitude > 0.0001f)//이동 멈춤 로직 이 때 공격 넣으면 될 듯
            moveDir = dir.normalized;
        else
            moveDir = Vector3.zero;
    }

    private void ApplyPhysicsMovement(Vector3 direction)
    {
        if(direction == Vector3.zero)
        {
            rb.linearVelocity = new Vector3(0f,rb.linearVelocity.y,0f);
            return;
        }

        Vector3 velocity = direction * moveSpeed;
        velocity.y = rb.linearVelocity.y;

        rb.linearVelocity = velocity;
    }

    private bool CheckStopDistance()
    {
        if (target == null || target.gameObject.activeInHierarchy)
            return true;
        
        float dist = Vector3.Distance(transform.position, transform.position);

        return dist <= stopDistance;
    }
}