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
        player.SetGravity(player.Gravity);
        // stop invincible
    }

    public override void UpdateState()
    {
        
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (player.JumpQueued && player.touchingGround && animEnded)
            return PlayerStateMachine.EPlayerState.Jump;
        
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
