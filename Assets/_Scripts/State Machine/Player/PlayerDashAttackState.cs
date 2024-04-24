using UnityEngine;

public class PlayerDashAttackState : PlayerState
{
    public PlayerDashAttackState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D rb, Animator anim) : base(key, entity, rb, anim)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        player.ConsumeAttackInput();
        
        player.SetVelocity(player.PlayerAttacks[2].AttackMoveDirection); // change to dash specific

        stateTimer = 0.1f;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        stateTimer -= Time.deltaTime;
        if (stateTimer <= 0)
        {
            player.ZeroVelocity();
        }
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (animEnded)
            return PlayerStateMachine.EPlayerState.Idle;

        return StateKey;
    }

    public override void AnimationFinishTrigger()
    {
        animEnded = true;
    }
}
