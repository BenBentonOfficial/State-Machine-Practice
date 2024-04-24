using UnityEngine;

public class PlayerIdleState : PlayerState
{

    public PlayerIdleState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D rb, Animator anim) : base(key, entity, rb, anim)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        player.ZeroVelocity();
        player.ResetDoubleJump();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
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
        
        if (InputManager.MovementInput().magnitude > 0)
        {
            return PlayerStateMachine.EPlayerState.Move;
        }
        
        return StateKey;
    }

    public override void AnimationFinishTrigger()
    {
        throw new System.NotImplementedException();
    }
}
