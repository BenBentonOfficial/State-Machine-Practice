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

    private float cooldownTime;

    public override void EnterState()
    {
        base.EnterState();
        cooldownTime = 3f;
        player.attack += Attack;
        
        player.ConsumeAttackInput();
        player.ConsumeAttack();
        player.ZeroVelocity();
        player.SetGravity(player.Gravity * 0.3f);
    }

    public override void ExitState()
    {
        base.ExitState();
        player.airAttackCooldown.SetTime(cooldownTime);
        player.StartAttackCooldown();
        player.attack -= Attack;
        player.SetGravity(player.Gravity);
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

        if (successfulHit)
        {
            player.ResetAttack();
            // MOVE TO BETTER PLACE
            cooldownTime = 0.2f;
        }
            

        
        
    }
}
