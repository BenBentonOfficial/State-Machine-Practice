public class PlayerStateMachine : StateManager<PlayerStateMachine.EPlayerState>
{
    public enum EPlayerState
    {
        Idle,
        Move,
        Action
    }

    private void Awake()
    {
        States.Add(EPlayerState.Idle, new PlayerIdleState(EPlayerState.Idle));
        States.Add(EPlayerState.Move, new PlayerMoveState(EPlayerState.Move));
        States.Add(EPlayerState.Action, new PlayerActionState(EPlayerState.Action));

        CurrentState = States[EPlayerState.Idle];
    }
}
