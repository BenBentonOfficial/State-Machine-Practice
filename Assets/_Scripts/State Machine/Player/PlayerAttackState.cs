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
    private AttackDirection savedDirection;
    private bool bounceHit = false;

    public override void EnterState()
    {
        base.EnterState();
        bounceHit = false;
        player.attack += Attack;
        player.ConsumeAttackInput();
        
        player.CheckFlip();
        player.NoFlip();
        //if (!player.touchingGround)
            //animEnded = true;

        // if (comboCount > 1 || Time.time > lastTimeAttacked + comboWindow)
        // {
        //     comboCount = 0;
        // }
        //
        // player.SetCurrentCombo(comboCount);
        //
        // if (InputManager.MovementInput().y > 0.4f)
        // {
        //     comboCount = 2;
        // }

        savedDirection  = player.attackDirection();
        player.SetVelocityX(0);
        
        if (savedDirection == AttackDirection.down)
        {
            Debug.Log(savedDirection);
            player.SetVelocityY(2f);
        }
            
        
        player.Animator.SetInteger("Direction", (int)savedDirection);
         //player.SetVelocity(player.PlayerAttacks[0].AttackMoveDirection);

        stateTimer = 0.1f;
    }

    public override void ExitState()
    {
        player.attack -= Attack;
        comboCount++;
        lastTimeAttacked = Time.time;
        //player.SetGravity(player.Gravity);
        //player.ZeroVelocity();
        player.ResetCanFlip();
        base.ExitState();
    }

    public override void UpdateState()
    {
        stateTimer -= Time.deltaTime;
        if (stateTimer > 0)
        {
            if (stateTimer <= 0)
            { 
                player.ResetCanFlip();
            }
        }

        if (player.hitStopped())
        { 
            player.ZeroVelocity();
        }
            
        
        else if(!InputManager.MovementInput().x.Equals(0))
            player.SetVelocityX((player.MoveSpeed *0.6f) * InputManager.MovementInput().x);
        
        //if(stateTimer < 0 && player.touchingGround)
            //player.ZeroVelocity();
        
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (bounceHit && animEnded)
            return PlayerStateMachine.EPlayerState.Jump;
        
        if (animEnded && !player.touchingGround)
        {
            return PlayerStateMachine.EPlayerState.Fall;
        }

        if (animEnded)
            return PlayerStateMachine.EPlayerState.Idle;

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
        var attackLocation = player.attackLocation();
        player.Attack();
        var hits = Physics2D.CircleCastAll(player.attackLocation(), 0.7f, attackTransform.right, 0f, layers);
        bool successfullHit = false;
        foreach (var hit in hits)
        {
            if (hit.transform.TryGetComponent<IHealth>(out IHealth health))
            {
                health.Damage(attacks[0].Damage, 5 * player.FacingDir);
                successfullHit = true;
            }
        }
        
        if(successfullHit && !player.touchingGround) 
        { 
            if (savedDirection == AttackDirection.down)
            {
                bounceHit = true;
                player.SetVelocityY(10f);
            }
            else
            {
                player.StartHitStop();
            }
        }
    }
    

}
