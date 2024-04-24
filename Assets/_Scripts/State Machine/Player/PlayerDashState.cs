using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D rb, Animator anim) : base(key, entity, rb, anim)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        player.CheckFlip();
        player.SetVelocity(player.DashVelocity);
        player.SetGravity(0f);
        //invincible
    }

    public override void ExitState()
    {
        base.ExitState();
        //Master.ZeroVelocity();
        player.SetGravity(4f);
        // stop invincible
    }

    public override void UpdateState()
    {
        
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (animEnded && player.touchingGround)
            return PlayerStateMachine.EPlayerState.Idle;

        if (animEnded && !player.touchingGround)
            return PlayerStateMachine.EPlayerState.Fall;

        if (player.AttackQueued && player.touchingGround)
            return PlayerStateMachine.EPlayerState.DashAttack;

        return StateKey;
    }

    public override void AnimationFinishTrigger()
    {
        animEnded = true;
    }
}
