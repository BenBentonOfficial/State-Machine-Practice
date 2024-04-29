using UnityEngine;

public class PlayerDashAttackState : PlayerState
{
    public PlayerDashAttackState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D rb, Animator anim) : base(key, entity, rb, anim)
    {
        attack = entity.DashAttack;
        attackTransform = entity.AttackTransform;

    }

    private PlayerAttackSO attack;
    private Transform attackTransform;

    public override void EnterState()
    {
        base.EnterState();
        player.attack += Attack;
        player.ConsumeAttackInput();
        
        player.SetVelocity(player.PlayerAttacks[2].AttackMoveDirection); // change to dash specific

        stateTimer = 0.1f;
    }

    public override void ExitState()
    {
        player.attack -= Attack;
        base.ExitState();
    }

    public override void UpdateState()
    {
        stateTimer -= Time.deltaTime;
        if (stateTimer <= 0)
        {
            player.ZeroVelocity();
        }
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
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
        var hits = Physics2D.CircleCastAll(attackTransform.position, 1, Vector2.zero);
        Debug.Log("Attack");
        foreach (var hit in hits)
        {
            Debug.Log(hit.transform.gameObject.name);

            if (hit.transform.TryGetComponent<IHealth>(out IHealth health))
            {
                health.Damage(attack.Damage, attack.Knockback * player.FacingDir);
            }

        }
    }
}
