using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D rb, Animator anim) : base(key, entity, rb, anim)
    {
    }

    private int comboCount = 0;
    private float lastTimeAttacked;
    private float comboWindow = 0.2f;
    
    private bool animEnded;

    public override void EnterState()
    {
        Master.ConsumeAttackInput();
        Master.Animator().SetBool(StateKey.ToString(), true);
        Master.CheckFlip();
        animEnded = false;
        if (!Master.touchingGround)
            animEnded = true;

        if (comboCount > 2 || Time.time > lastTimeAttacked + comboWindow)
        {
            comboCount = 0;
        }
        
        Master.Animator().SetInteger("Combo", comboCount);
        Master.SetVelocity(Master.PlayerAttacks()[comboCount].AttackMoveDirection);

        stateTimer = 0.1f;
    }

    public override void ExitState()
    {
        comboCount++;
        lastTimeAttacked = Time.time;
        Master.Animator().SetBool(StateKey.ToString(), false);
    }

    public override void UpdateState()
    {
        stateTimer -= Time.deltaTime;
        if (stateTimer <= 0)
        {
            Master.ZeroVelocity();
        }
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (animEnded)
        {
            return PlayerStateMachine.EPlayerState.Idle;
        }

        return StateKey;
    }

    public override void AnimationFinishTrigger()
    {
        animEnded = true;
    }


}
