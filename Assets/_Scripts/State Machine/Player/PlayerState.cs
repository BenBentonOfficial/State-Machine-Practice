using UnityEngine;

public class PlayerState : BaseState<PlayerStateMachine.EPlayerState>
{


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

    public PlayerState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D rb, Animator anim) : base(key, rb, anim)
    {
    }
}
