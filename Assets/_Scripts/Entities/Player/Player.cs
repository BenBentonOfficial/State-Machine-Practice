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
    private bool doubleJumpConsumed = false;

    [SerializeField] private List<PlayerAttackSO> playerAttacks;
    public List<PlayerAttackSO> PlayerAttacks() => playerAttacks;

    public bool CanDoubleJump()
    {
        return !doubleJumpConsumed;
    }

    public void ConsumeDoubleJump()
    {
        doubleJumpConsumed = true;
    }

    public void ResetDoubleJump()
    {
        doubleJumpConsumed = false;
    }

    #endregion

    #region Input Control
    
    private void QueueAttackInput()
    {
        inputConsumeTimer = 0.4f;
        attackQueued = true;
    }

    private void QueueJumpInput()
    {
        inputConsumeTimer = 0.1f;
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

    public override void CheckFlip()
    {
        if (InputManager.MovementInput().x < 0 && facingDirection == 1)
        {
            Flip(180f);
        }
        else if (InputManager.MovementInput().x > 0 && facingDirection == -1)
        {
            Flip(0f);
        }
    }
}
