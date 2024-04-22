using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    

    private void AnimationTrigger()
    {
        PlayerComponents.StateMachine().GetCurrentState().AnimationFinishTrigger();
    }
}
