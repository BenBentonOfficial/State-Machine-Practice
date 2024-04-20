using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [Header("Input Actions")] 
    [SerializeField] private InputActionReference movement;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public static Vector2 MovementInput()
    {
        return instance.movement.action.ReadValue<Vector2>();
    }
    
}
