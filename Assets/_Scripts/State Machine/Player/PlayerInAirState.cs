using UnityEngine;

public class PlayerInAirState : BaseState<PlayerStateMachine.EPlayerState>
{
    public PlayerInAirState(PlayerStateMachine.EPlayerState key) : base(key)
    {
    }

    public override void EnterState()
    {
        Debug.Log("in air");
        PlayerComponents.Animator().SetBool("Jump", true);
        PlayerComponents.Rigidbody().gravityScale = 1.5f;
    }

    public override void ExitState()
    {
        PlayerComponents.Animator().SetBool("Jump", false);
        PlayerComponents.Rigidbody().gravityScale = 4f;
    }

    public override void UpdateState()
    {
        if(!InputManager.MovementInput().x.Equals(0))
            PlayerComponents.SetVelocityX(2f * InputManager.MovementInput().x);
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (PlayerComponents.TouchingGround())
            return PlayerStateMachine.EPlayerState.Idle;
        
        return StateKey;
    }

    public override void AnimationFinishTrigger()
    {
        throw new System.NotImplementedException();
    }
}
