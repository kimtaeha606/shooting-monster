using NUnit.Framework;
using Unity.Android.Gradle;
using Unity.FPS.Game;
using UnityEditor.VersionControl;
using UnityEngine;

public sealed class MonsterAI : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private Transform target;

    [Header("Movement (Physics)")]
    [SerializeField] private Rigidbody rb;          // 3D 기준. 2D면 Rigidbody2D로 교체.
    
    [SerializeField] private float stopDistance = 1.2f;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private string isMovingParam = "IsMoving";
    
    [Header("Ref")]
    [SerializeField] private MonsterDef def;
    [SerializeField] private MonsterAttack attack;
    [SerializeField] private Collider attackCollider;

    [Header("Runtime (Read Only)")]
    [SerializeField] private Vector3 moveDir = Vector3.zero;
    [SerializeField] private bool hasTarget = false;
    [SerializeField] private bool isMoving = false;
    private bool isAttacking=false;

    
    private void Update()
    {
        if (!AcquireTarget())
        {
            moveDir = Vector3.zero;
            SetMoving(false);
            return;
        }

        bool inRange = CheckStopDistance();

        // 이동 bool은 매 프레임 갱신 (권장) 
        animator.SetBool("InRange", inRange);

        // 공격 중이면 이동/공격 재시작하지 않게 막기
        if (isAttacking)
        {
            moveDir = Vector3.zero;
            SetMoving(false);
            return;
        }

        // 사거리 안 + 현재 공격 중 아님 => 공격 1회 발동
        if (inRange)
        {
            moveDir = Vector3.zero;
            SetMoving(false);

            isAttacking = true;
            animator.SetTrigger("Attack");
            return;
        }

        // 사거리 밖 => 추적
        UpdateMoveDirection();
        SetMoving(moveDir.sqrMagnitude > 0.0001f);
    }

    
    private void FixedUpdate()
    {
        float moveSpeed = def != null ? def.moveSpeed : 0f;
        ApplyPhysicsMovement(moveDir, moveSpeed);
    }

   

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
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            moveDir = Vector3.zero;
            return;
        }

        Vector3 dir = target.position - transform.position;

        dir.y = 0f;

        if (dir.sqrMagnitude > 0.0001f)
            moveDir = dir.normalized;
        else
            moveDir = Vector3.zero;
    }

    private void ApplyPhysicsMovement(Vector3 direction, float moveSpeed)
    {
        if(direction == Vector3.zero)
        {
            rb.linearVelocity = new Vector3(0f,0f,0f);
            return;
        }

        Vector3 velocity = direction * moveSpeed;
        velocity.y = 0f;

        rb.linearVelocity = velocity;
    }

    private bool CheckStopDistance()
    {
        if (target == null || !target.gameObject.activeInHierarchy)
            return true;
        
        float dist = Vector3.Distance(transform.position, target.position);

        return dist <= stopDistance;
    }

    private void SetMoving(bool moving)
    {
        if (isMoving == moving) return;
        isMoving = moving;

        if (animator && !string.IsNullOrEmpty(isMovingParam))
            animator.SetBool(isMovingParam, isMoving); 
    }

    public void SetAttacking(bool v) => isAttacking = v;

    private void OnTriggerEnter(Collider other)
    {
        if (!attackCollider.enabled) return;

        var dmg = other.GetComponent<Damageable>();
        if (dmg)
            attack.TryHit(dmg);
    }

}
