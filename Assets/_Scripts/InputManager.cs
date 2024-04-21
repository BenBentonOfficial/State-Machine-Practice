using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [Header("Input Actions")] 
    [SerializeField] private InputActionReference movement;

    [SerializeField] private InputActionReference attack;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        attack.action.performed += context => Attack(context);
    }

    public static Vector2 MovementInput()
    {
        return instance.movement.action.ReadValue<Vector2>();
    }

    private void Attack(InputAction.CallbackContext context)
    {
    }
}
