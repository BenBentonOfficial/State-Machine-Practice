using UnityEngine;

public class PlayerJumpState : BaseState<PlayerStateMachine.EPlayerState>
{

    private Player Master;

    public override void EnterState()
    {
        Master.ConsumeJumpInput();
        Master.SetVelocityY(Master.JumpForce());
        Master.Animator().SetBool(StateKey.ToString(), true);
    }

    public override void ExitState()
    {
        Master.Animator().SetBool(StateKey.ToString(), false);
    }

    public override void UpdateState()
    {
        if(!InputManager.MovementInput().x.Equals(0))
            Master.SetVelocityX(2f * InputManager.MovementInput().x);
        
        Master.CheckFlip();
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        //Debug.Log(PlayerComponents.Rigidbody().velocity.y);
        if (Master.Velocity().y <= 0)
        {
            Debug.Log("Trying to change state");
            return PlayerStateMachine.EPlayerState.InAir;
        }
            
        
        if (Master.AttackQueued())
        {
            return PlayerStateMachine.EPlayerState.Attack;
        }

        if (Master.JumpQueued())
        {
            return PlayerStateMachine.EPlayerState.Jump;
        }

        return StateKey;
    }

    public override void AnimationFinishTrigger()
    {
        throw new System.NotImplementedException();
    }

    public PlayerJumpState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D rb, Animator anim) : base(key, rb, anim)
    {
        Master = entity;
    }
}
