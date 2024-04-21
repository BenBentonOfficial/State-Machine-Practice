public class PlayerStateMachine : StateManager<PlayerStateMachine.EPlayerState>
{
    public enum EPlayerState
    {
        Idle,
        Move,
        Action,
        Attack,
        InAir
    }

    private void Awake()
    {
        States.Add(EPlayerState.Idle, new PlayerIdleState(EPlayerState.Idle));
        States.Add(EPlayerState.Move, new PlayerMoveState(EPlayerState.Move));
        States.Add(EPlayerState.Action, new PlayerActionState(EPlayerState.Action));
        States.Add(EPlayerState.Attack, new PlayerAttackState(EPlayerState.Attack));

        CurrentState = States[EPlayerState.Idle];
    }
}
