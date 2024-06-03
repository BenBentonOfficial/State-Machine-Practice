using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    public EnemyPatrolState(EnemyStateMachine.EEnemyState key, Enemy entity, Rigidbody2D rb, Animator anim) : base(key, entity, rb, anim)
    {
    }


    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
        enemy.ZeroVelocity();
    }

    public override void UpdateState()
    {
        enemy.SetVelocityX(enemy.FacingDir * enemy.moveSpeed);
    }

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        if (!enemy.ledgeCheck.Detected())
            return EnemyStateMachine.EEnemyState.Idle;

        return StateKey;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
