using System;
using UnityEngine;

public class PlayerComponents : MonoBehaviour
{
    public static PlayerComponents instance;

    private static Rigidbody2D rb;
    private static Animator animator;
    private static PlayerStateMachine _stateMachine;

    private int facingDir = 1;

    private static bool attackQueued;
    private static bool jumpQueued;
    private float inputConsumeTimer;
    public bool touchingGround;

    [SerializeField] private PlayerAttackSO[] playerAttacks;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundLayer;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        _stateMachine = GetComponent<PlayerStateMachine>();

        InputManager.instance.attackAction += QueueAttackInput;
        InputManager.instance.jumpAction += QueueJumpInput;
    }
    
    

    public int FacingDirection() => facingDir;

    public static bool AttackQueued() => attackQueued;
    public static bool JumpQueued() => jumpQueued;

    private void QueueJumpInput()
    {
        instance.inputConsumeTimer = 0.2f;
        jumpQueued = true; 
    }
    public static void ConsumeJumpInput() => jumpQueued = false;

    private void QueueAttackInput()
    {
        instance.inputConsumeTimer = 0.35f;
        attackQueued = true;
    }

    public static void ConsumeAttackInput() => attackQueued = false;

    public static Rigidbody2D Rigidbody() => rb;

    public static Animator Animator() => animator;

    public static PlayerStateMachine StateMachine() => _stateMachine;

    public PlayerAttackSO[] PlayerAttacks() => playerAttacks;

    public float JumpForce() => jumpForce;

    public static void ZeroVelocity() => rb.velocity = Vector2.zero;

    public static void SetVelocity(Vector2 newVelocity) => rb.velocity = new Vector2(newVelocity.x * instance.facingDir, newVelocity.y);

    public static void SetVelocityX(float newXVelocity) => rb.velocity = new Vector2(newXVelocity, rb.velocityY);
    public static void SetVelocityY(float newYVelocity) => rb.velocity = new Vector2(rb.velocityX, newYVelocity);
    private void Update()
    {
        inputConsumeTimer -= Time.deltaTime;
        if (inputConsumeTimer <= 0)
        {
            ConsumeAttackInput();
            ConsumeJumpInput();
        }
        CheckFlip();
        touchingGround = TouchingGround();
        Animator().SetFloat("yVelocity", rb.velocityY);
    }

    // possibly move to individual states
    private void CheckFlip()
    {
        if (InputManager.MovementInput().x < 0 && facingDir == 1)
        {
            Flip(180f);
        }
        else if (InputManager.MovementInput().x > 0 && facingDir == -1)
        {
            Flip(0f);
        }
    }

    private void Flip(float newRot)
    {
        Vector3 newDir = new Vector3(transform.rotation.x, newRot, transform.rotation.z);
        transform.rotation = Quaternion.Euler(newDir);
        facingDir *= -1;
    }

    public static bool TouchingGround() =>
        Physics2D.Raycast(instance.transform.position, Vector2.down, instance.groundCheckDistance, instance.groundLayer);

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance, 0));
    }
}
