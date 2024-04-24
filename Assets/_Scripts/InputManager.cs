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
        attack.action.performed += Attack;
        jump.action.performed += Jump;
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
        jumpAction?.Invoke();
    }
    
}
