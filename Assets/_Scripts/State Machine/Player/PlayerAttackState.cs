using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D rb, Animator anim) : base(key, entity, rb, anim)
    {
    }

    private int comboCount = 0;
    private float lastTimeAttacked;
    private float comboWindow = 0.2f;

    public override void EnterState()
    {
        base.EnterState();
        player.ConsumeAttackInput();
        
        player.CheckFlip();
        if (!player.touchingGround)
            animEnded = true;

        if (comboCount > 2 || Time.time > lastTimeAttacked + comboWindow)
        {
            comboCount = 0;
        }
        
        player.Animator.SetInteger("Combo", comboCount);
        player.SetVelocity(player.PlayerAttacks[comboCount].AttackMoveDirection);

        stateTimer = 0.1f;
    }

    public override void ExitState()
    {
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
        animEnded = true;
    }


}
