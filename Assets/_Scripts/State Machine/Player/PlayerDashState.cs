using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D rb, Animator anim) : base(key, entity, rb, anim)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Master.CheckFlip();
        Master.SetVelocity(Master.DashVelocity);
        Master.SetGravity(0f);
        //invincible
    }

    public override void ExitState()
    {
        base.ExitState();
        //Master.ZeroVelocity();
        Master.SetGravity(4f);
    }

    public override void UpdateState()
    {
        
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (animEnded)
            return PlayerStateMachine.EPlayerState.Idle;

        if (Master.AttackQueued && Master.touchingGround)
            return PlayerStateMachine.EPlayerState.DashAttack;

        return StateKey;
    }

    public override void AnimationFinishTrigger()
    {
        animEnded = true;
    }
}
