using UnityEngine;

[CreateAssetMenu(menuName = "Player/NewAttack")]
public class PlayerAttackSO : ScriptableObject
{
    public int Damage;
    public Vector2 AttackMoveDirection;
}
