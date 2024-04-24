using UnityEngine;

public class PlayerFallState : PlayerState
{
    public PlayerFallState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D rb, Animator anim) : base(key, entity, rb, anim)
    {
    }

    private float coyoteTimer = 0;

    public override void EnterState()
    {
        base.EnterState();
        if (player.StateManager.GetLastState().StateKey == PlayerStateMachine.EPlayerState.Move)
        {
            Debug.Log("coyote time");
            coyoteTimer = 0.2f;
        }
        player.SetGravity(1.5f);
    }

    public override void ExitState()
    {
        base.ExitState();
        player.SetGravity(4f);
    }

    public override void UpdateState()
    {
        if(!InputManager.MovementInput().x.Equals(0))
            player.SetVelocityX(2f * InputManager.MovementInput().x);

        
        if (coyoteTimer > 0)
        {
           coyoteTimer -= Time.deltaTime;
        }
        
        
        player.CheckFlip();
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (player.touchingGround)
            return PlayerStateMachine.EPlayerState.Idle;

        if (!player.touchingGround && coyoteTimer > 0 && player.JumpQueued)
        {
            player.ConsumeDoubleJump();
            return PlayerStateMachine.EPlayerState.Jump;
        }

        if (!player.touchingGround && player.JumpQueued && player.CanDoubleJump())
            return PlayerStateMachine.EPlayerState.DoubleJump;
        
        
        return StateKey;
    }

    public override void AnimationFinishTrigger()
    {
        throw new System.NotImplementedException();
    }

    
}
