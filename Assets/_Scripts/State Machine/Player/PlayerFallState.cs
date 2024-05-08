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
        stateTimer = 1;
        coyoteTimer = 0;
        if (player.StateManager.GetLastState().StateKey == PlayerStateMachine.EPlayerState.Move)
        {
            Debug.Log("coyote time");
            coyoteTimer = 0.2f;
        }
        
        //player.SetGravity(player.Gravity / 1.75f);
    }

    public override void ExitState()
    {
        base.ExitState();
        player.SetGravity(player.Gravity);
    }

    public override void UpdateState()
    {
        if(!InputManager.MovementInput().x.Equals(0))
            player.SetVelocityX((player.MoveSpeed *0.8f) * InputManager.MovementInput().x);
        
        player.SetGravity(RB.gravityScale += Time.deltaTime * 3f);
        
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
