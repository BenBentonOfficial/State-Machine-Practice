using UnityEngine;

public class PlayerComponents : MonoBehaviour
{
    public static PlayerComponents instance;

    private static Rigidbody2D rigidbody;
    private static Animator animator;
    private static PlayerStateMachine _stateMachine;

    private int facingDir = 1;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        _stateMachine = GetComponent<PlayerStateMachine>();
    }

    public static Rigidbody2D Rigidbody() => rigidbody;

    public static Animator Animator() => animator;

    public static PlayerStateMachine StateMachine() => _stateMachine;

    public static void ZeroVelocity() => rigidbody.velocity = Vector2.zero;

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
