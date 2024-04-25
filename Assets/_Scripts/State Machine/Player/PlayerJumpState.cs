using UnityEngine;

public class PlayerJumpState : PlayerState
{

    public PlayerJumpState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D rb, Animator anim) : base(key, entity, rb, anim)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        player.ConsumeJumpInput();
        player.SetVelocityY(player.JumpForce);
        stateTimer = 0.1f; // coyote time
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

        if (player.JumpQueued && player.CanDoubleJump())
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
