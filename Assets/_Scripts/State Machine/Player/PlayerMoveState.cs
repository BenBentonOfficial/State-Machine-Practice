public class PlayerMoveState : BaseState<PlayerStateMachine.EPlayerState>
{

    public PlayerMoveState(PlayerStateMachine.EPlayerState key) : base(key)
    {
    }

    public override void EnterState()
    {
        PlayerComponents.Animator().SetBool(StateKey.ToString(), true);
    }

    public override void ExitState()
    {
        PlayerComponents.ZeroVelocity();
        PlayerComponents.Animator().SetBool(StateKey.ToString(), false);
    }

    public override void UpdateState()
    {
        PlayerComponents.Rigidbody().velocity = InputManager.MovementInput();
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (InputManager.MovementInput().magnitude <= 0)
        {
            return PlayerStateMachine.EPlayerState.Idle;
        }
        return StateKey;
    }

    public override void AnimationFinishTrigger()
    {
        throw new System.NotImplementedException();
    }
}
