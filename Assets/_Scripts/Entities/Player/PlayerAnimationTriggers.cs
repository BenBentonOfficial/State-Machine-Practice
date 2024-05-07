using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void AnimationEndTrigger()
    {
        player.StateManager.GetCurrentState().AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        player.attack?.Invoke();
        //Instantiate(player.PlayerAttacks[player.CurrentCombo()].slashAnim, player.AttackTransform.position, transform.rotation);
    }
}
