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
        Master.ZeroVelocity();
    }

    public override void UpdateState()
    {
        Master.SetVelocityX(InputManager.MovementInput().x * 3f);
        Master.CheckFlip();
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (InputManager.MovementInput().magnitude <= 0)
        {
            return PlayerStateMachine.EPlayerState.Idle;
        }
        
        if (Master.AttackQueued)
        {
            return PlayerStateMachine.EPlayerState.Attack;
        }

        if (Master.JumpQueued)
        {
            return PlayerStateMachine.EPlayerState.Jump;
        }

        if (!Master.touchingGround)
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
