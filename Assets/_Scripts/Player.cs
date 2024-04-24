using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    #region Setup
    
    public PlayerStateMachine StateManager { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        StateManager = GetComponent<PlayerStateMachine>();
        StateManager.Initialize(this, rb, animator);

        InputManager.instance.attackAction += QueueAttackInput;
        InputManager.instance.jumpAction += QueueJumpInput;
    }
    #endregion

    #region Variables

    private bool attackQueued;
    private bool jumpQueued;
    private float inputConsumeTimer;

    [SerializeField] private List<PlayerAttackSO> playerAttacks;
    public List<PlayerAttackSO> PlayerAttacks() => playerAttacks;

    #endregion

    #region Input Control
    
    private void QueueAttackInput()
    {
        inputConsumeTimer = 0.4f;
        attackQueued = true;
    }

    private void QueueJumpInput()
    {
        inputConsumeTimer = 0.2f;
        jumpQueued = true;
    }

    public void ConsumeAttackInput() => attackQueued = false;
    public void ConsumeJumpInput() => jumpQueued = false;
    public bool AttackQueued() => attackQueued;
    public bool JumpQueued() => jumpQueued;
    
    #endregion

    private void Update()
    {
        inputConsumeTimer -= Time.deltaTime;
        if (inputConsumeTimer <= 0)
        {
            ConsumeAttackInput();
            ConsumeJumpInput();
        }
    }
}
