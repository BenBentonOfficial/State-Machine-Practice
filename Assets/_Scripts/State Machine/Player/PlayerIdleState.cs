using UnityEngine;

public class PlayerIdleState : BaseState<PlayerStateMachine.EPlayerState>
{

    private Player Master;

    public override void EnterState()
    {
        //attacking = false;
        Master.Animator().SetBool(StateKey.ToString(), true);
        //InputManager.instance.attackAction += RegisterAttackInput;
    }

    public override void ExitState()
    {
        Master.Animator().SetBool(StateKey.ToString(), false);
        //InputManager.instance.attackAction -= RegisterAttackInput;
    }

    public override void UpdateState()
    {
        //throw new System.NotImplementedException();
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

    public PlayerIdleState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D rb, Animator anim) : base(key, rb, anim)
    {
        Master = entity;
    }
}
