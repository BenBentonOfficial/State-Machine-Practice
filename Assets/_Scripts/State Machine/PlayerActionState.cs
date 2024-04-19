using UnityEngine;

public class PlayerActionState : BaseState<PlayerStateMachine.EPlayerState>
{
    public PlayerActionState(PlayerStateMachine.EPlayerState key) : base(key)
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
}
