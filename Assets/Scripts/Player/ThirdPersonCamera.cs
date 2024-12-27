using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform playerHolder;
    [SerializeField] private Transform player;
    [SerializeField] private float rotationSpeed;

    
    private Vector2 _inputDirection;
    private CursorLockMode _previousCursorLockMode;

    public void SetInputDirection(Vector2 input) => _inputDirection = input;

    private void FixedUpdate()
    {
        var playerPosition = playerHolder.position;
        var camPosition = transform.position;
        var viewDir = playerPosition - new Vector3(camPosition.x, playerPosition.y, camPosition.z);
        orientation.forward = viewDir.normalized;

        var inputDir = orientation.forward * _inputDirection.y + orientation.right * _inputDirection.x;

        player.forward = Vector3.Slerp(player.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
    }
}
