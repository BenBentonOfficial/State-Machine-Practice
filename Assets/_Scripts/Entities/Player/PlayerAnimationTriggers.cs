using System;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player Master;

    private void Awake()
    {
        Master = GetComponentInParent<Player>();
    }

    private void AnimationTrigger()
    {
        Master.StateManager.GetCurrentState().AnimationFinishTrigger();
    }
}
