using UnityEngine;

public class PlayerMoveState : PlayerState
{

    public PlayerMoveState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D rb, Animator anim) : base(key, entity, rb, anim)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
        player.ZeroVelocity();
    }

    public override void UpdateState()
    {
        player.SetVelocityX(InputManager.MovementInput().x * 3f);
        player.CheckFlip();
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (InputManager.MovementInput().magnitude <= 0)
        {
            return PlayerStateMachine.EPlayerState.Idle;
        }
        
        if (player.AttackQueued)
        {
            return PlayerStateMachine.EPlayerState.Attack;
        }

        if (player.JumpQueued)
        {
            return PlayerStateMachine.EPlayerState.Jump;
        }

        if (player.DashQueued)
        {
            return PlayerStateMachine.EPlayerState.Dash;
        }

        if (!player.touchingGround)
        {
            return PlayerStateMachine.EPlayerState.Fall;
        }
        
        return StateKey;
    }

    public override void AnimationFinishTrigger()
    {
        throw new System.NotImplementedException();
    }


    
}
