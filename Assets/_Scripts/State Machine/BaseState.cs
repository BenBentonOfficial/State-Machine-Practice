using System;using UnityEngine;

public abstract class BaseState<EState>
    where EState : Enum
{
    protected BaseState(EState key, Rigidbody2D rb, Animator anim)
    {
        StateKey = key;
        RB = rb;
        Animator = anim;
    }

    //TODO: move playercomponents references here. Change playercomponents to hold player stats. 
    // use animation trigger events to finish some states, like attacking
    // add state timer so some states can have lengths (stun, stopping velocity change when attacking)

    public EState StateKey { get; set; }
    public Rigidbody2D RB { get; set; }
    public Animator Animator { get; set; }

    protected float stateTimer;

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
    public abstract EState GetNextState(); // EState is a generic type // logic for checking next state.

    public abstract void AnimationFinishTrigger();
}
