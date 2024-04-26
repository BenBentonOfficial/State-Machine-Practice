using UnityEngine;

public class Entity : MonoBehaviour, IHealth
{
    protected Rigidbody2D rb;
    protected Animator animator;

    protected int facingDirection = 1;

    [SerializeField] protected float jumpForce;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask groundLayer;

    [SerializeField] protected Transform attackTransform;
    private int maxHealth;
    private bool invincible;


    public bool touchingGround =>
        Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        groundLayer = LayerMask.GetMask("Ground");
    }

    public void ZeroVelocity() => rb.velocity = Vector2.zero;
    public void SetVelocity(Vector2 newVelocity) => rb.velocity = new Vector2(newVelocity.x * facingDirection, newVelocity.y);
    public void SetVelocityX(float newXVelocity) => rb.velocity = new Vector2(newXVelocity, rb.velocityY);
    public void SetVelocityY(float newYVelocity) => rb.velocity = new Vector2(rb.velocityX, newYVelocity);
    public Vector2 Velocity() => rb.velocity;

    public void SetGravity(float newGrav) => rb.gravityScale = newGrav;

    public virtual void CheckFlip()
    {
        if (rb.velocityX < 0 && facingDirection == 1)
        {
            Flip(180f);
        }
        else if (rb.velocityX > 0 && facingDirection == -1)
        {
            Flip(0f);
        }
    }

    protected virtual void Flip(float newRot)
    {
        Vector3 newDir = new Vector3(transform.rotation.x, newRot, transform.rotation.z);
        transform.rotation = Quaternion.Euler(newDir);
        facingDirection *= -1;
    }

    private void OnDrawGizmosSelected()
    {
        var groundCheckEndPoint = new Vector3(transform.position.x, transform.position.y - groundCheckDistance, 0f);
        Gizmos.DrawLine(transform.position, groundCheckEndPoint);
    }

    public Animator Animator => animator;
    public float JumpForce => jumpForce;
    public int FacingDir => facingDirection;

    public void Damage(int damage, float knockbackPwr)
    {
        throw new System.NotImplementedException();
    }

    public void ToggleInvincible()
    {
        throw new System.NotImplementedException();
    }

    #region Health

    protected int _currentHealth;
    protected int _maxHealth;

    public int Current => _currentHealth;
    public int Max => _maxHealth;
    public void Set(int value)
    {
        _currentHealth = value;
    }

    public void Heal(int value)
    {
        _currentHealth += value;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
    }

    public void Damage(int value)
    {
        _currentHealth -= value;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        Debug.Log("Damaged");
    }
    
    #endregion
}
