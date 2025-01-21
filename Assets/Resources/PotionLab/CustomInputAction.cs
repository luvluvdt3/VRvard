using UnityEngine;
using UnityEngine.InputSystem;

public class CustomInputAction : MonoBehaviour
{
    public InputActionReference customButton;
    public GameObject target;
    void Start()
    {
        customButton.action.started += ButtonWasPressed;
        customButton.action.canceled += ButtonWasReleased;
    }
    
    void ButtonWasPressed(InputAction.CallbackContext context)
    {
        target.SetActive(true);
    }
    
    void ButtonWasReleased(InputAction.CallbackContext context)
    {
        target.SetActive(false);
    }
}
