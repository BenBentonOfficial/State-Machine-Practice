using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D rb, Animator anim) : base(key, entity, rb, anim)
    {
        attacks = entity.PlayerAttacks;
        attackTransform = entity.AttackTransform;
    }

    private int comboCount = 0;
    private float lastTimeAttacked;
    private float comboWindow = 0.2f;
    private List<PlayerAttackSO> attacks;
    private Transform attackTransform;

    public override void EnterState()
    {
        base.EnterState();
        player.attack += Attack;
        player.ConsumeAttackInput();
        
        player.CheckFlip();
        if (!player.touchingGround)
            animEnded = true;

        if (comboCount > 1 || Time.time > lastTimeAttacked + comboWindow)
        {
            comboCount = 0;
        }
        
        player.SetCurrentCombo(comboCount);
        
        player.Animator.SetInteger("Combo", comboCount);
        player.SetVelocity(player.PlayerAttacks[comboCount].AttackMoveDirection);

        stateTimer = 0.1f;
    }

    public override void ExitState()
    {
        player.attack -= Attack;
        comboCount++;
        lastTimeAttacked = Time.time;
        player.SetGravity(player.Gravity);
        player.ZeroVelocity();
        base.ExitState();
    }

    public override void UpdateState()
    {
        stateTimer -= Time.deltaTime;
        if (stateTimer > 0)
        {
            if (stateTimer <= 0)
            { 
                player.ZeroVelocity();
            }
        }
        
        if(stateTimer < 0 && player.touchingGround)
            player.ZeroVelocity();
        
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (animEnded)
        {
            return PlayerStateMachine.EPlayerState.Idle;
        }

        //if (!player.touchingGround)
            //return PlayerStateMachine.EPlayerState.Fall;

        return StateKey;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public void Attack()
    {
        var hits = Physics2D.CircleCastAll(attackTransform.position, 1, Vector2.zero, 0, layers);
       
        foreach (var hit in hits)
        {
            Debug.Log(hit.transform.gameObject.name);

            if (hit.transform.TryGetComponent<IHealth>(out IHealth health))
            {
                health.Damage(attacks[comboCount].Damage, attacks[comboCount].Knockback * player.FacingDir);
            }

        }
    }

}
