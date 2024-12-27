using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    
    [SerializeField] private UnityEvent<Vector2> onPlayerMovement;
    [SerializeField] private UnityEvent onPlayerJump;
    [SerializeField] private UnityEvent<bool> onPlayerSprint;
    
    private void Update()
    {
        HandleMovement();
        HandleJump();
        HandleSprint();
    }
    
    private void HandleMovement()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput = Input.GetAxisRaw("Vertical");
        var inputDir = new Vector2(horizontalInput, verticalInput);

        onPlayerMovement?.Invoke(inputDir);
    }
    
    private void HandleJump()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            onPlayerJump?.Invoke();
        }
    }
    
    private void HandleSprint() => onPlayerSprint?.Invoke(Input.GetKey(sprintKey));
}
