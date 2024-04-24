using UnityEngine;

public class PlayerDoubleJumpState : PlayerState
{
    public PlayerDoubleJumpState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D rb, Animator anim) : base(key, entity, rb, anim)
    {
    }


    public override void EnterState()
    {
        base.EnterState();
        Master.ConsumeJumpInput();
        Master.ConsumeDoubleJump();
        Master.SetVelocityY(Master.JumpForce); // add doubleJump to animator
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        if(!InputManager.MovementInput().x.Equals(0))
            Master.SetVelocityX(2f * InputManager.MovementInput().x);

        Master.CheckFlip();
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (Master.Velocity().y <= 0)
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
