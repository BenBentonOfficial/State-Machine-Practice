using UnityEngine;

public class PlayerDoubleJumpState : PlayerState
{
    public PlayerDoubleJumpState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D rb, Animator anim) : base(key, entity, rb, anim)
    {
    }


    public override void EnterState()
    {
        base.EnterState();
        player.ConsumeJumpInput();
        player.ConsumeDoubleJump();
        player.SetVelocityY(player.JumpForce * 0.85f); // add doubleJump to animator
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        if(!InputManager.MovementInput().x.Equals(0))
            player.SetVelocityX(player.MoveSpeed * InputManager.MovementInput().x);

        player.CheckFlip();
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (player.Velocity().y <= 0)
        {
            return PlayerStateMachine.EPlayerState.Fall;
        }

        return StateKey;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
