using ControlStrategies;
using Enums;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] public float moveSpeed;

    [SerializeField] private ControlStrategyAbstract standardFpControlStrategy;
    [SerializeField] private ControlStrategyAbstract directionalControlStrategy;
    [SerializeField] private ControlType controlType;

    private Rigidbody _rb;
    public Transform orientation;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }

    private void Update()
    {
        switch (controlType)
        {
            case ControlType.DirectionalControlStrategy:
                directionalControlStrategy.ReadPlayerMovement();
                break;
            case ControlType.StandardFpControlStrategy:
                standardFpControlStrategy.ReadPlayerMovement();
                break;
            default:
                standardFpControlStrategy.ReadPlayerMovement();
                break;
        }

        SpeedControl();
    }

    private void FixedUpdate()
    {
        switch (controlType)
        {
            case ControlType.DirectionalControlStrategy:
                directionalControlStrategy.UpdatePlayerMovement(orientation, _rb, moveSpeed);
                break;
            case ControlType.StandardFpControlStrategy:
                standardFpControlStrategy.UpdatePlayerMovement(orientation, _rb, moveSpeed);
                break;
            default:
                standardFpControlStrategy.UpdatePlayerMovement(orientation, _rb, moveSpeed);
                break;
        }
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