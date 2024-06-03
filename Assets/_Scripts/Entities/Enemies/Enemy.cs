using UnityEngine;

public class Enemy : Entity
{
    public EnemyStateMachine StateManager { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        StateManager = GetComponent<EnemyStateMachine>();
        StateManager.Initialize(this, rb, animator);
    }

    public RayCollisionCheck ledgeCheck;

    //Move to SO
    #region EnemyStats

    public float moveSpeed;


    #endregion
}
