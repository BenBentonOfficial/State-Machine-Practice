using UnityEngine;

public class PlayerMoveState : BaseState<PlayerStateMachine.EPlayerState>
{

    private Player Master;

    public override void EnterState()
    {
        Master.Animator().SetBool(StateKey.ToString(), true);
    }

    public override void ExitState()
    {
        Master.ZeroVelocity();
        Master.Animator().SetBool(StateKey.ToString(), false);
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
        
        if (Master.AttackQueued())
        {
            return PlayerStateMachine.EPlayerState.Attack;
        }

        if (Master.JumpQueued())
        {
            return PlayerStateMachine.EPlayerState.Jump;
        }

        if (!Master.touchingGround)
        {
            return PlayerStateMachine.EPlayerState.InAir;
        }
        
        return StateKey;
    }

    public override void AnimationFinishTrigger()
    {
        throw new System.NotImplementedException();
    }

    public PlayerMoveState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D rb, Animator anim) : base(key, rb, anim)
    {
        Master = entity;
    }
}
