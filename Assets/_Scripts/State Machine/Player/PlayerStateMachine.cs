using UnityEngine;

public class PlayerStateMachine : StateManager<PlayerStateMachine.EPlayerState>
{
    public enum EPlayerState
    {
        Idle,
        Move,
        Attack,
        InAir,
        Jump,
        DoubleJump
    }
    
    public void Initialize(Player entity, Rigidbody2D rb, Animator anim)
    {
        States.Add(EPlayerState.Idle, new PlayerIdleState(EPlayerState.Idle, entity, rb, anim));
        States.Add(EPlayerState.Move, new PlayerMoveState(EPlayerState.Move, entity, rb, anim));
        States.Add(EPlayerState.Attack, new PlayerAttackState(EPlayerState.Attack, entity, rb, anim));
        States.Add(EPlayerState.Jump, new PlayerJumpState(EPlayerState.Jump, entity, rb, anim));
        States.Add(EPlayerState.DoubleJump, new PlayerDoubleJumpState(EPlayerState.DoubleJump, entity, rb, anim));
        States.Add(EPlayerState.InAir, new PlayerInAirState(EPlayerState.InAir, entity, rb, anim));

        CurrentState = States[EPlayerState.Idle];
    }
}
