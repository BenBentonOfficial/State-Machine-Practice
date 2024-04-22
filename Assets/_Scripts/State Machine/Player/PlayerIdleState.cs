using UnityEngine;

public class PlayerIdleState : BaseState<PlayerStateMachine.EPlayerState>
{
    public PlayerIdleState(PlayerStateMachine.EPlayerState key) : base(key)
    {
    }

    public override void EnterState()
    {
        //attacking = false;
        PlayerComponents.Animator().SetBool(StateKey.ToString(), true);
        //InputManager.instance.attackAction += RegisterAttackInput;
    }

    public override void ExitState()
    {
        PlayerComponents.Animator().SetBool(StateKey.ToString(), false);
        //InputManager.instance.attackAction -= RegisterAttackInput;
    }

    public override void UpdateState()
    {
        //throw new System.NotImplementedException();
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (PlayerComponents.AttackQueued())
        {
            return PlayerStateMachine.EPlayerState.Attack;
        }

        if (PlayerComponents.JumpQueued())
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
