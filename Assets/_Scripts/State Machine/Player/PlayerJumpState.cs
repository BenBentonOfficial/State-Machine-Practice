using UnityEngine;

public class PlayerJumpState : PlayerState
{

    public PlayerJumpState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D rb, Animator anim) : base(key, entity, rb, anim)
    {
    }

    public override void EnterState()
    {
        Master.ConsumeJumpInput();
        Master.SetVelocityY(Master.JumpForce());
        Master.Animator().SetBool(StateKey.ToString(), true);
        stateTimer = 0.1f; // coyote time
    }

    public override void ExitState()
    {
        Master.Animator().SetBool(StateKey.ToString(), false);
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
            return PlayerStateMachine.EPlayerState.InAir;
        }

        if (Master.JumpQueued() && Master.CanDoubleJump())
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
