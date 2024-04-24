using UnityEngine;

public class PlayerJumpState : PlayerState
{

    public PlayerJumpState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D rb, Animator anim) : base(key, entity, rb, anim)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Master.ConsumeJumpInput();
        Master.SetVelocityY(Master.JumpForce);
        stateTimer = 0.1f; // coyote time
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

        if (Master.JumpQueued && Master.CanDoubleJump())
        {
            return PlayerStateMachine.EPlayerState.DoubleJump;
        }

        return StateKey;
    }

    public override void AnimationFinishTrigger()
    {
        throw new System.NotImplementedException();
    }


    
}
