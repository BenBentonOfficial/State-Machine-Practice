using UnityEngine;

public class PlayerStateMachine : StateManager<PlayerStateMachine.EPlayerState>
{
    public enum EPlayerState
    {
        Idle,
        Move,
        Attack,
        Fall,
        Jump,
        DoubleJump,
        Dash,
        DashAttack,
        AirAttack
    }
    
    public void Initialize(Player entity, Rigidbody2D rb, Animator anim)
    {
        States.Add(EPlayerState.Idle, new PlayerIdleState(EPlayerState.Idle, entity, rb, anim));
        States.Add(EPlayerState.Move, new PlayerMoveState(EPlayerState.Move, entity, rb, anim));
        States.Add(EPlayerState.Attack, new PlayerAttackState(EPlayerState.Attack, entity, rb, anim));
        States.Add(EPlayerState.Jump, new PlayerJumpState(EPlayerState.Jump, entity, rb, anim));
        States.Add(EPlayerState.DoubleJump, new PlayerDoubleJumpState(EPlayerState.DoubleJump, entity, rb, anim));
        States.Add(EPlayerState.Fall, new PlayerFallState(EPlayerState.Fall, entity, rb, anim));
        States.Add(EPlayerState.Dash, new PlayerDashState(EPlayerState.Dash, entity, rb, anim));
        States.Add(EPlayerState.DashAttack, new PlayerDashAttackState(EPlayerState.DashAttack, entity, rb, anim));
        States.Add(EPlayerState.AirAttack, new PlayerAirAttackState(EPlayerState.AirAttack, entity, rb, anim));

        CurrentState = States[EPlayerState.Idle];
    }
}
