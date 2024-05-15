using System;
using System.Collections.Generic;
using UnityEngine;
public enum AttackDirection
    {
        left,
        right,
        up,
        down
    }
public class Player : Entity
{
    [SerializeField] private Transform[] attackTransforms;

    public bool attackFlipDir = false;
    private bool canFlip = true;

    public void NoFlip() => canFlip = false;
    public void ResetCanFlip() => canFlip = true;

    public AttackDirection attackDirection()
    {
        var y = InputManager.MovementInput().y;
        var x = InputManager.MovementInput().x;
        if (y > 0.4f && Mathf.Abs(y) > Mathf.Abs(x))
        {
            return AttackDirection.up;
        }
        else if (y < -0.4f && Mathf.Abs(y) > Mathf.Abs(x))
        {
            return AttackDirection.down;
        }

        if (attackFlipDir)
        {
            attackFlipDir = !attackFlipDir;
            return AttackDirection.left;
        }

        attackFlipDir = !attackFlipDir;
        return AttackDirection.right;
    }

    public Vector2 attackLocation()
    {
        attackDirection(); // this is dumb figure this out better
        switch (attackDirection())
        { case AttackDirection.left:
                return attackTransforms[0].position;
            case AttackDirection.right:
                return attackTransforms[0].position;
            case AttackDirection.up:
                return attackTransforms[1].position;
            case AttackDirection.down:
                return attackTransforms[2].position;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }      
    
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
        attackCooldown = new Timer();
        airAttackCooldown = new Timer();
        onDamage += StartHitStop;
    }
    #endregion

    #region Variables

    private bool attackQueued;
    private bool jumpQueued;
    private bool dashQueued;
    
    private float inputConsumeTimer;
    private bool doubleJumpConsumed = false;
    private bool attackConsumed = false;

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

    #region Attack

    public bool CanAttack()
    {
        return !attackConsumed && attackCooldown.TimerFinished;
    }

    public void ConsumeAttack()
    {
        attackConsumed = true;
    }

    public void ResetAttack()
    {
        attackConsumed = false;
    }
    

    #endregion



    #region Timers

    public Timer dashCooldown;
    public Timer attackCooldown;
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
        if (!canFlip)
            return;
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

    public void StartAttackCooldown()
    {
        StartCoroutine(attackCooldown.StartTimer());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position - new Vector3(0, groundCheckDistance, 0));
        Gizmos.DrawWireSphere(attackTransform.position, 0.7f);
        //Gizmos.DrawWireSphere(attackTransform.position + attackTransform.right * 1f, 0.7f);
    }

    public void Attack()
    {
         //Instantiate(playerAttacks[currentCombo].slashAnim,attackTransform, worldPositionStays:false);
    }
    
    
}
