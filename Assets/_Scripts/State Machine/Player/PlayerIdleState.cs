public class PlayerIdleState : BaseState<PlayerStateMachine.EPlayerState>
{
    public PlayerIdleState(PlayerStateMachine.EPlayerState key) : base(key)
    {
    }

    public override void EnterState()
    {
        PlayerComponents.Animator().SetBool(StateKey.ToString(), true);
    }

    public override void ExitState()
    {
        PlayerComponents.Animator().SetBool(StateKey.ToString(), false);
    }

    public override void UpdateState()
    {
        //throw new System.NotImplementedException();
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (InputManager.MovementInput().magnitude > 0)
        {
            return PlayerStateMachine.EPlayerState.Move;
        }
        return StateKey;
    }
}
