using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public Transform orientation;

    private float _horizontalInput;
    private float _verticalInput;

    private Vector3 _moveDirection;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }

    private void Update()
    {
        MyInput();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        _moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;
        _rb.AddForce(_moveDirection.normalized * (moveSpeed * 30f), ForceMode.Acceleration);
    }

    private void SpeedControl()
    {
        var velocity = _rb.velocity;
        var speedVec = new Vector3(velocity.x, 0f, velocity.z);
        if (speedVec.magnitude > moveSpeed)
        {
            var limitedSpeed = speedVec.normalized * moveSpeed;
            _rb.velocity = new Vector3(limitedSpeed.x, velocity.y, limitedSpeed.z);
        }
    }
}
