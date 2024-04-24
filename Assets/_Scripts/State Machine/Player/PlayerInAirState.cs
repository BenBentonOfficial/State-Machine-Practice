using UnityEngine;

public class PlayerInAirState : BaseState<PlayerStateMachine.EPlayerState>
{
    //TODO: Change gravity scale to a setting in playerComponents. Also, change playerComponents to instead have each state get a copy
    private Player Master;

    public override void EnterState()
    {
        Master.Animator().SetBool("Jump", true);
        Master.SetGravity(1.5f);
    }

    public override void ExitState()
    {
        Master.Animator().SetBool("Jump", false);
        Master.SetGravity(4f);
    }

    public override void UpdateState()
    {
        if(!InputManager.MovementInput().x.Equals(0))
            Master.SetVelocityX(2f * InputManager.MovementInput().x);
        
        Master.CheckFlip();
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (Master.touchingGround)
            return PlayerStateMachine.EPlayerState.Idle;
        
        return StateKey;
    }

    public override void AnimationFinishTrigger()
    {
        throw new System.NotImplementedException();
    }

    public PlayerInAirState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D rb, Animator anim) : base(key, rb, anim)
    {
        Master = entity;
    }
}
