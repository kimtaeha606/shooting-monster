using UnityEngine;

public class AttackStateBehaviour : StateMachineBehaviour
{
    // Attack 상태에 진입할 때
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // MonsterAI 같은 컴포넌트를 Animator가 붙은 오브젝트에서 찾음
        var ai = animator.GetComponent<MonsterAI>();
        if (ai != null) ai.SetAttacking(true);
    }

    // Attack 상태에서 빠져나올 때(전이 시작 시점에 호출)
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var ai = animator.GetComponent<MonsterAI>();
        if (ai != null) ai.SetAttacking(false);
    }
}
