using UnityEngine;

public class PlayerState : BaseState<PlayerStateMachine.EPlayerState>
{
    public PlayerState(PlayerStateMachine.EPlayerState key) : base(key)
    {
    }

    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        throw new System.NotImplementedException();
    }

    public override void AnimationFinishTrigger()
    {
        throw new System.NotImplementedException();
    }
}
