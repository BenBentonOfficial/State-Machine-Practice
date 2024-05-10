using UnityEngine;

public class PlayerAirAttackState : PlayerState
{
    public PlayerAirAttackState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D rb, Animator anim) : base(key, entity, rb, anim)
    {
        attack = entity.DashAttack; // change to air later
        attackTransform = entity.AttackTransform;
    }

    private PlayerAttackSO attack;
    private Transform attackTransform;

    public override void EnterState()
    {
        base.EnterState();
        player.attack += Attack;
        
        player.ConsumeAttackInput();
        player.ConsumeAirAttack();
        player.ZeroVelocity();

    }

    public override void ExitState()
    {
        base.ExitState();
        player.attack -= Attack;
    }
    public override void UpdateState()
    {
        player.ZeroVelocity();
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (animEnded && !player.touchingGround)
            return PlayerStateMachine.EPlayerState.Fall;

        if (animEnded)
            return PlayerStateMachine.EPlayerState.Idle;

        return StateKey;
    }

    public override void AnimationFinishTrigger()
    {
        animEnded = true;
    }
    
    public void Attack()
    {
        var hits = Physics2D.CircleCastAll(attackTransform.position, 1, Vector2.zero, 0, layers);
        bool successfulHit = false;
        foreach (var hit in hits)
        {
            if (hit.transform.TryGetComponent<IHealth>(out IHealth health))
            {
                health.Damage(attack.Damage, attack.Knockback * player.FacingDir);
                successfulHit = true;
            }
        }

        
        
    }
}
