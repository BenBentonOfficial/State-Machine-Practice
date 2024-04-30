using UnityEngine;

[CreateAssetMenu(menuName = "Player/NewAttack")]
public class PlayerAttackSO : ScriptableObject
{
    public int Damage;
    public float Knockback;
    public Vector2 AttackMoveDirection;

    public GameObject slashAnim;
}
