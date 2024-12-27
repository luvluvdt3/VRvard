using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float swayAmount = 0.5f; // Amount of swaying motion
    [SerializeField] private float swaySpeedMultiplier = 2f; // Speed multiplier for the sway motion
    [SerializeField] private float shakeAmount = 0.1f; // Amount of shaking when moving
    [SerializeField] private float shakeSpeed = 5f; // Speed of shaking when moving
    [SerializeField] private float faceRotationSpeed = 5f;
    [SerializeField] private float cameraFollowSpeed = 5f;
    [SerializeField] private float cameraDefaultYPosition;

    private Transform _transform;
    private Vector2 _input;
    private Camera _camera;
    private float _defaultFieldOfView;

    private void Awake()
    {
        _camera = Camera.main;
        _transform = transform;
    }

    public void SetInput(Vector2 input) => _input = input;

    private void Start()
    {
        _defaultFieldOfView = _camera.fieldOfView;
    }

    private void FixedUpdate()
    {
        var position = player.transform.position;
        
        if (_input.y > 0)
        {
            var sinusoidalOffset = Mathf.Sin(Time.time * swaySpeedMultiplier) * swayAmount;
            position.y = cameraDefaultYPosition * sinusoidalOffset;
            var shakeOffsetX = Mathf.PerlinNoise(Time.time * shakeSpeed, 0) * shakeAmount;
            var shakeOffsetZ = Mathf.PerlinNoise(0, Time.time * shakeSpeed) * shakeAmount;
            position.x += shakeOffsetX;
            position.z += shakeOffsetZ;
        }
        
        _transform.position = Vector3.Lerp(_transform.position, position, Time.deltaTime * cameraFollowSpeed);

        var targetRotation = Quaternion.Euler(_input.x * faceRotationSpeed, player.eulerAngles.y, _input.x * faceRotationSpeed);
        transform.localRotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void ChangeCameraFieldOfView(Vector2 input)
    {
        _camera.fieldOfView = Mathf.MoveTowards(_camera.fieldOfView, _defaultFieldOfView + input.y * 15, Time.deltaTime * 10);
    }
}
