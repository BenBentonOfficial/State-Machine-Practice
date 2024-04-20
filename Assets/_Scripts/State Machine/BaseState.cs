using System;
using UnityEngine;

public abstract class BaseState<EState> where EState : Enum
{
    public BaseState(EState key)
    {
        StateKey = key;
    }
    
    public EState StateKey { get; set; }

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
    public abstract EState GetNextState(); // EState is a generic type // logic for checking next state. Also runs every frame, but after update. 
}
