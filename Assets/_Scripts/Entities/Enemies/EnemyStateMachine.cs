using UnityEngine;

public class EnemyStateMachine : StateManager<EnemyStateMachine.EEnemyState>
{
    public enum EEnemyState
    {
        Idle,
        Patrol,
        Chase,
        AttackCharge,
        Attack,
        Flinch
    }

    public void Initialize(Enemy entity, Rigidbody2D rb, Animator anim)
    {
        States.Add(EEnemyState.Idle, new EnemyIdleState(EEnemyState.Idle, entity, rb, anim));
    }
    
    
}
