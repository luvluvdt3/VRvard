using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float groundDrag;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    [SerializeField] private float turnSpeed;

    [Header("Ground Check")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform controlOrientation;
    
    private bool _readyToJump;
    private Vector2 _inputDirection;
    private Vector3 _moveDirection;
    private Rigidbody _rb;

    private float _currentMoveSpeed;
    
    public bool IsGrounded { get; private set; }
    public float JumpCooldown => jumpCooldown;

    public void SetMovementDirection(Vector2 input) => _inputDirection = input;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _readyToJump = true;
        _currentMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, groundLayer);
        
        ControlSpeed();
        _rb.linearDamping = IsGrounded ? groundDrag : 0;
    }

    private void FixedUpdate() => MovePlayer();

    public void StartJump()
    {
        if (!_readyToJump || !IsGrounded)
        {
            return;
        }
        
        _readyToJump = false;

        Jump();

        Invoke(nameof(ResetJump), jumpCooldown);
    }

    private void MovePlayer()
    {
        _moveDirection = (orientation.forward * _inputDirection.y + orientation.right * _inputDirection.x).normalized;
        var forward = controlOrientation.forward;
        forward = Vector3.Slerp(new Vector3(forward.x, 0, forward.z), _moveDirection, Time.deltaTime * turnSpeed);
        controlOrientation.forward = forward;
        _rb.AddForce(_moveDirection * (_currentMoveSpeed * 10f * (IsGrounded ? 1 : airMultiplier)), ForceMode.Force);
    }

    private void ControlSpeed()
    {
        var velocity = _rb.linearVelocity;
        
        var flatVelocity = new Vector3(velocity.x, 0f, velocity.z);
        
        var limitedVelocity = flatVelocity.normalized * _currentMoveSpeed;

        _rb.linearVelocity = _inputDirection == Vector2.zero ? new Vector3(0, velocity.y, 0) : new Vector3(limitedVelocity.x, velocity.y, limitedVelocity.z);
    }

    private void Jump()
    {
        _rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    public void Sprint(bool isSprinting) => _currentMoveSpeed = isSprinting ? moveSpeed * 2 : moveSpeed;
    private void ResetJump() => _readyToJump = true;
}