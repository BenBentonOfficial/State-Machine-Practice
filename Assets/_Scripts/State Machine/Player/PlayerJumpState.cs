using UnityEngine;

public class PlayerJumpState : PlayerState
{

    public PlayerJumpState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D rb, Animator anim) : base(key, entity, rb, anim)
    {
    }

    private bool jumpInputEnded = false;

    private void EndJump()
    {
        jumpInputEnded = true;
        //Debug.Log("Ending jump");
    }

    public override void EnterState()
    {
        base.EnterState();
        jumpInputEnded = false;
        InputManager.instance.jumpActionEnd += EndJump;
        player.ConsumeJumpInput();

        /*if (player.StateManager.GetLastState().StateKey == PlayerStateMachine.EPlayerState.Dash)
        {
            player.SetVelocityY(player.JumpForce * 1.5f);
            player.SetVelocityX(player.Velocity().x / 4);
        }
        else
        {
            player.SetVelocityY(player.JumpForce);
        }*/
        
        player.SetVelocityY(player.JumpForce);
        
     
            

        //Debug.Log(player.Velocity());
        stateTimer = 0.1f; // coyote time
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
        
        player.SetGravity(RB.gravityScale += Time.deltaTime * 5f);

        player.CheckFlip();
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (player.Velocity().y <= 0 || jumpInputEnded)
        {
            return PlayerStateMachine.EPlayerState.Fall;
        }

        if (player.JumpQueued && player.CanDoubleJump())
        {
            return PlayerStateMachine.EPlayerState.DoubleJump;
        }

        if (player.AttackQueued && player.CanAirAttack())
        {
            return PlayerStateMachine.EPlayerState.AirAttack;
        }
        
        return StateKey;
    }

    public override void AnimationFinishTrigger()
    {
        throw new System.NotImplementedException();
    }


    
}
