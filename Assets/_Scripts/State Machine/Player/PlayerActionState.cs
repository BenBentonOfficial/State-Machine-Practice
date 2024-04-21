using UnityEngine;

public class PlayerActionState : BaseState<PlayerStateMachine.EPlayerState>
{
    private bool actionAnimEnded;
    public PlayerActionState(PlayerStateMachine.EPlayerState key) : base(key)
    {
    }

    public override void EnterState()
    {
        actionAnimEnded = false;
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
        if (actionAnimEnded)
        {
            return PlayerStateMachine.EPlayerState.Idle;
        }
        else
        {
            return StateKey;
        }
    }

    public override void AnimationFinishTrigger()
    {
        actionAnimEnded = true;
    }
}
