using UnityEngine;

public class PlayerComponents : MonoBehaviour
{
    public static PlayerComponents instance;

    private static Rigidbody2D rb;
    private static Animator animator;
    private static PlayerStateMachine _stateMachine;

    private int facingDir = 1;

    private static bool attackQueued;

    [SerializeField] private PlayerAttackSO[] playerAttacks;
    
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
    }

    public static bool AttackQueued() => attackQueued;

    private void QueueAttackInput() => attackQueued = true;

    public static void ConsumeAttackInput() => attackQueued = false;

    public static Rigidbody2D Rigidbody() => rb;

    public static Animator Animator() => animator;

    public static PlayerStateMachine StateMachine() => _stateMachine;

    public PlayerAttackSO[] PlayerAttacks()
    {
        return instance.playerAttacks;
    }

    public static void ZeroVelocity() => rb.velocity = Vector2.zero;

    public static void SetVelocity(Vector2 newVelocity) => rb.velocity = newVelocity;

    private void Update()
    {
        CheckFlip();
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
}
