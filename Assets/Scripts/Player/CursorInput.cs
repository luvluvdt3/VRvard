using UnityEngine;

public class CursorInput : MonoBehaviour
{
    private CursorLockMode _previousCursorLockMode = CursorLockMode.Locked;
    
    public void ToggleCursorLock()
    {
        Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = !Cursor.visible;
        _previousCursorLockMode = Cursor.lockState;
    }
    
    public void SetCursorLock(bool isLocked)
    {
        Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isLocked;
    }
    
    public void SetCursorToPreviousState()
    {
        Cursor.lockState = _previousCursorLockMode;
        Cursor.visible = _previousCursorLockMode == CursorLockMode.None;
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
