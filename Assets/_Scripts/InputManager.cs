using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [Header("Input Actions")] 
    [SerializeField] private InputActionReference movement;

    [SerializeField] private InputActionReference attack;

    [SerializeField] private InputActionReference jump;

    public Action attackAction;
    public Action jumpAction;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        attack.action.performed += context => Attack(context);
        jump.action.performed += context => Jump(context);
    }

    public static Vector2 MovementInput()
    {
        return instance.movement.action.ReadValue<Vector2>();
    }

    private void Attack(InputAction.CallbackContext context)
    {
        attackAction?.Invoke();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Trying to jump");
        jumpAction?.Invoke();
    }
    
}
