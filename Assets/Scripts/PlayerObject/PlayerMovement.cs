using ControlStrategies;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] public float moveSpeed;

    [SerializeField] private ControlTypeWrapper controlTypeWrapper;

    private Rigidbody _rb;
    public Transform orientation;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }

    private void Update()
    {
        controlTypeWrapper.GetControlStrategy().ReadPlayerMovement();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        controlTypeWrapper.GetControlStrategy().UpdatePlayerMovement(orientation, _rb, moveSpeed);
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