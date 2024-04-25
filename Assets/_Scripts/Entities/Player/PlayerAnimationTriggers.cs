using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player Master;

    private void Awake()
    {
        Master = GetComponentInParent<Player>();
    }

    private void AnimationEndTrigger()
    {
        Master.StateManager.GetCurrentState().AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        
    }
}
