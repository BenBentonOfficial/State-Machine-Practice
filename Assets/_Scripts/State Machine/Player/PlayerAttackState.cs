using UnityEngine;

public class PlayerAttackState : BaseState<PlayerStateMachine.EPlayerState>
{
    public PlayerAttackState(PlayerStateMachine.EPlayerState key) : base(key)
    {
    }

    private int comboCount = 0;
    private float lastTimeAttacked;
    private float comboWindow = 0.5f;

    private float stateTimer;
    private bool animEnded;

    public override void EnterState()
    {
        PlayerComponents.Animator().SetBool(StateKey.ToString(), true);
        animEnded = false;

        if (comboCount > 1 || Time.time > lastTimeAttacked + comboWindow)
        {
            comboCount = 0;
        }
        
        PlayerComponents.Animator().SetInteger("Combo", comboCount);

        stateTimer = 0.1f;
    }

    public override void ExitState()
    {
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
        animEnded = true;
    }
}
