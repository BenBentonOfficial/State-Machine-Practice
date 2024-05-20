using UnityEngine;

public class EnemyState : BaseState<EnemyStateMachine.EEnemyState>
{
    public EnemyState(EnemyStateMachine.EEnemyState key, Enemy entity, Rigidbody2D rb, Animator anim) : base(key, rb, anim)
    {
        enemy = entity;
    }

    protected Enemy enemy;
    protected LayerMask layers = LayerMask.GetMask("Friendly");

    public override void EnterState()
    {
        enemy.Animator.SetBool(StateKey.ToString(), true);
        animEnded = false;
    }

    public override void ExitState()
    {
        enemy.Animator.SetBool(StateKey.ToString(), false);
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        throw new System.NotImplementedException();
    }

    public override void AnimationFinishTrigger()
    {
        animEnded = true;
    }
}
