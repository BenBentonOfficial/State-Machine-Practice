using UnityEngine;

public class PlayerState : BaseState<PlayerStateMachine.EPlayerState>
{

    protected Player player;
    protected LayerMask layers = LayerMask.GetMask("Enemy");

    public override void EnterState()
    {
        player.Animator.SetBool(StateKey.ToString(), true);
        animEnded = false;
    }

    public override void ExitState()
    {
        player.Animator.SetBool(StateKey.ToString(), false);
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
        animEnded = true;
    }

    protected PlayerState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D rb, Animator anim) : base(key, rb, anim)
    {
        player = entity;
    }
}
