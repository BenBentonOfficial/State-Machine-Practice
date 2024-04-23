using UnityEngine;

public class PlayerJumpState : BaseState<PlayerStateMachine.EPlayerState>
{
    public PlayerJumpState(PlayerStateMachine.EPlayerState key) : base(key)
    {
    }

    public override void EnterState()
    {
        PlayerComponents.ConsumeJumpInput();
        PlayerComponents.SetVelocityY(PlayerComponents.instance.JumpForce());
        PlayerComponents.Animator().SetBool(StateKey.ToString(), true);
    }

    public override void ExitState()
    {
        PlayerComponents.Animator().SetBool(StateKey.ToString(), false);
    }

    public override void UpdateState()
    {
        if(!InputManager.MovementInput().x.Equals(0))
            PlayerComponents.SetVelocityX(2f * InputManager.MovementInput().x);
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        //Debug.Log(PlayerComponents.Rigidbody().velocity.y);
        if (PlayerComponents.Rigidbody().velocity.y <= 0)
        {
            Debug.Log("Trying to change state");
            return PlayerStateMachine.EPlayerState.InAir;
        }
            
        
        if (PlayerComponents.AttackQueued())
        {
            return PlayerStateMachine.EPlayerState.Attack;
        }

        if (PlayerComponents.JumpQueued())
        {
            return PlayerStateMachine.EPlayerState.Jump;
        }

        return StateKey;
    }

    public override void AnimationFinishTrigger()
    {
        throw new System.NotImplementedException();
    }
}
