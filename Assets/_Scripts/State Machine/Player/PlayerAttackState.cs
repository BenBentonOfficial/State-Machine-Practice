using UnityEngine;

public class PlayerAttackState : BaseState<PlayerStateMachine.EPlayerState>
{
    public PlayerAttackState(PlayerStateMachine.EPlayerState key) : base(key)
    {
    }

    private int comboCount = 0;
    private float lastTimeAttacked;
    private float comboWindow = 0.2f;

    private float stateTimer;
    private bool animEnded;

    public override void EnterState()
    {
        //attackQueued = false;
        PlayerComponents.ConsumeAttackInput();
        PlayerComponents.Animator().SetBool(StateKey.ToString(), true);
        animEnded = false;

        if (comboCount > 2 || Time.time > lastTimeAttacked + comboWindow)
        {
            comboCount = 0;
        }
        
        PlayerComponents.Animator().SetInteger("Combo", comboCount);
        PlayerComponents.SetVelocity(PlayerComponents.instance.PlayerAttacks()[comboCount].AttackMoveDirection);

        stateTimer = 0.1f;
    }

    public override void ExitState()
    {
        //Debug.Log(PlayerComponents.AttackQueued());
        comboCount++;
        lastTimeAttacked = Time.time;
        PlayerComponents.Animator().SetBool(StateKey.ToString(), false);
    }

    public override void UpdateState()
    {
        stateTimer -= Time.deltaTime;
        if (stateTimer <= 0)
        {
            PlayerComponents.ZeroVelocity();
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
        Debug.Log("Trying to end anim");
        animEnded = true;
    }
}
