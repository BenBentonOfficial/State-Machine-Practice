using UnityEngine;

public class PlayerIdleState : PlayerState
{

    public PlayerIdleState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D rb, Animator anim) : base(key, entity, rb, anim)
    {
    }

    public override void EnterState()
    {
        Master.ZeroVelocity();
        Master.ResetDoubleJump();
        Master.Animator().SetBool(StateKey.ToString(), true);
    }

    public override void ExitState()
    {
        Master.Animator().SetBool(StateKey.ToString(), false);
    }

    public override void UpdateState()
    {
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (Master.AttackQueued())
        {
            return PlayerStateMachine.EPlayerState.Attack;
        }

        if (Master.JumpQueued())
        {
            return PlayerStateMachine.EPlayerState.Jump;
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
