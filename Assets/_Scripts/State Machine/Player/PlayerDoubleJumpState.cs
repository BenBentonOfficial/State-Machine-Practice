using UnityEngine;

public class PlayerDoubleJumpState : PlayerState
{
    public PlayerDoubleJumpState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D rb, Animator anim) : base(key, entity, rb, anim)
    {
    }
    
      private bool jumpInputEnded = false;
  
      private void EndJump()
      {
          jumpInputEnded = true;
      }  


    public override void EnterState()
    {
        base.EnterState();
        player.ConsumeJumpInput();
        player.ConsumeDoubleJump();
        
        jumpInputEnded = false;
        InputManager.instance.jumpActionEnd += EndJump;
        
        player.SetVelocityY(player.JumpForce * 0.85f); // add doubleJump to animator
    }

    public override void ExitState()
    {
        base.ExitState();
        player.SetVelocityY(0);
        InputManager.instance.jumpActionEnd -= EndJump;
    }

    public override void UpdateState()
    {
        if(!InputManager.MovementInput().x.Equals(0))
            player.SetVelocityX(player.MoveSpeed * InputManager.MovementInput().x);

        player.CheckFlip();
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (player.Velocity().y <= 0 || jumpInputEnded)
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
