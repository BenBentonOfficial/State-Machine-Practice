using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
        InputManager.instance.dashAction += QueueDashInput;

        dashCooldown = new Timer();
        airAttackCooldown = new Timer();
    }
    #endregion

    #region Variables

    private bool attackQueued;
    private bool jumpQueued;
    private bool dashQueued;
    
    private float inputConsumeTimer;
    private bool doubleJumpConsumed = false;

    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 dashVelocity;
    [SerializeField] private List<PlayerAttackSO> playerAttacks;
    [SerializeField] private PlayerAttackSO dashAttack;
    [SerializeField] private float gravity;

    private int currentCombo = 0;
    public void SetCurrentCombo(int value) => currentCombo = value;
    public int CurrentCombo() => currentCombo;
    public List<PlayerAttackSO> PlayerAttacks => playerAttacks;
    public PlayerAttackSO DashAttack => dashAttack;
    public Transform AttackTransform => attackTransform;
    public Vector2 DashVelocity => dashVelocity;
    public float Gravity => gravity;
    public float MoveSpeed => moveSpeed;

    public Action attack;
    
    #endregion

    #region Double Jump
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

    #region Timers

    public Timer dashCooldown;
    public Timer airAttackCooldown;

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

    private void QueueDashInput()
    {
        Debug.Log("Dash Queued");
        inputConsumeTimer = 0.1f;
        dashQueued = true;
    }


    public void ConsumeAttackInput() => attackQueued = false;
    public void ConsumeJumpInput() => jumpQueued = false;

    public void ConsumeDashInput() => dashQueued = false;
    public bool AttackQueued => attackQueued;
    public bool JumpQueued => jumpQueued;

    public bool DashQueued => dashQueued;
    
    #endregion

    private void Update()
    {
        inputConsumeTimer -= Time.deltaTime;
        if (inputConsumeTimer <= 0)
        {
            ConsumeAttackInput();
            ConsumeJumpInput();
            ConsumeDashInput();
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

    #region Attacks


    #endregion

    public void StartDashCooldown()
    {
        StartCoroutine(dashCooldown.StartTimer());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position - new Vector3(0, groundCheckDistance, 0));
        Gizmos.DrawWireSphere(attackTransform.position, 1);
    }
    
    
}
