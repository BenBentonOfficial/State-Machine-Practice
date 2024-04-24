using UnityEngine;

public class PlayerInAirState : PlayerState
{
    public PlayerInAirState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D rb, Animator anim) : base(key, entity, rb, anim)
    {
    }

    private float coyoteTimer = 0;

    public override void EnterState()
    {
        if (Master.StateManager.GetLastState().StateKey == PlayerStateMachine.EPlayerState.Move)
        {
            Debug.Log("coyote time");
            coyoteTimer = 0.2f;
        }
        Master.Animator().SetBool("Jump", true);
        Master.SetGravity(1.5f);
    }

    public override void ExitState()
    {
        Master.Animator().SetBool("Jump", false);
        Master.SetGravity(4f);
    }

    public override void UpdateState()
    {
        if(!InputManager.MovementInput().x.Equals(0))
            Master.SetVelocityX(2f * InputManager.MovementInput().x);

        
        if (coyoteTimer > 0)
        {
           coyoteTimer -= Time.deltaTime;
        }
        
        
        Master.CheckFlip();
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (Master.touchingGround)
            return PlayerStateMachine.EPlayerState.Idle;

        if (!Master.touchingGround && coyoteTimer > 0 && Master.JumpQueued())
        {
            Master.ConsumeDoubleJump();
            return PlayerStateMachine.EPlayerState.Jump;
        }

        if (!Master.touchingGround && Master.JumpQueued() && Master.CanDoubleJump())
            return PlayerStateMachine.EPlayerState.DoubleJump;
        
        
        return StateKey;
    }

    public override void AnimationFinishTrigger()
    {
        throw new System.NotImplementedException();
    }

    
}
