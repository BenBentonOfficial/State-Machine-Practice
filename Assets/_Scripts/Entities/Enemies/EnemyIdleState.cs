using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(EnemyStateMachine.EEnemyState key, Enemy entity, Rigidbody2D rb, Animator anim) : base(key, entity, rb, anim)
    {
    }


    public override void EnterState()
    {
        base.EnterState();
        enemy.ZeroVelocity();

        stateTimer = 2f;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        stateTimer -= Time.deltaTime;

        if (stateTimer >= 0)
        {
            if (stateTimer <= 0 )
            {
                
            }
        }
    }

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        return base.GetNextState();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
