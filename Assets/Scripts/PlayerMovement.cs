using System;
using ControlStrategies;
using Enums;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] public float moveSpeed;

    [SerializeField] private StandardFpControlStrategy standardFpControlStrategy;
    [SerializeField] private DirectionalControlStrategy directionalControlStrategy;

    public Transform orientation;

    private Vector3 _moveDirection;
    private Vector3 _startPos;
    private Vector3 _endPos;
    private float _fractionWay;

    private Rigidbody _rb;

    private bool _keyLock;
    private bool _keyForward;
    private bool _keyBack;
    private Direction _lastDirection;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }

    private void Update()
    {
        // standardFpControlStrategy.ReadPlayerMovement();
        DirectionalMove();

        SpeedControl();
    }

    private void FixedUpdate()
    {
        // standardFpControlStrategy.UpdatePlayerMovement(orientation, _rb, moveSpeed);
        MovePlayer();
    }

    private void DirectionalMove()
    {
        _keyForward = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        _keyBack = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        if ((_keyForward || _keyBack) && !_keyLock)
        {
            _keyLock = true;
            _lastDirection = _keyForward ? Direction.Up : Direction.Down;
            var position = transform.position;
            _startPos = position;
            _endPos = _lastDirection == Direction.Up ? position + orientation.forward : position - orientation.forward;
            _fractionWay = 0;
        }
    }

    private void MovePlayer()
    {
        if (!IsKeyPressed)
        {
            _rb.velocity = Vector3.zero;
        }
        else
        {
            if (_lastDirection == Direction.Up) _fractionWay += 0.06f;
            else _fractionWay -= 0.06f;
            transform.position = Vector3.Lerp(_startPos, _endPos, _fractionWay);
            if (CompareVector3(transform.position, _endPos)) _keyLock = false;
        }
    }

    private bool IsKeyPressed => _keyForward || _keyBack || _keyLock;

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

    private static bool CompareVector3(Vector3 first, Vector3 second)
        => !(Math.Abs(first.x - second.x) > 0.005f) && 
           !(Math.Abs(first.y - second.y) > 0.005f) &&
           !(Math.Abs(first.z - second.z) > 0.005f);
}