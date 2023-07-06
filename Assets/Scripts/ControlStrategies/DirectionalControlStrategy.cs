using System;
using Enums;
using UnityEngine;

namespace ControlStrategies
{
    public class DirectionalControlStrategy : ControlStrategyAbstract
    {
        private bool _lockInput;
        private bool _keyLeft;
        private bool _keyRight;
        private bool _keyForward;
        private bool _keyBack;
        private Direction _lastDirection;

        private Vector3 _moveDirection;
        private Vector3 _startPos;
        private Vector3 _endPos;
        private float _fractionWay;

        private float _finRotation;

        public override void UpdateRotation(ref float xRotation, ref float yRotation)
        {
            _keyLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
            _keyRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

            if ((_keyLeft || _keyRight) && !_lockInput)
            {
                _lockInput = true;
                _finRotation = _keyLeft ? yRotation - 90f : yRotation + 90f;
                _lastDirection = _keyLeft ? Direction.Left : Direction.Right;
            }

            if (!_lockInput) return;
            switch (_lastDirection)
            {
                case Direction.Left:
                    yRotation -= 1f;
                    if (yRotation <= _finRotation) _lockInput = false;
                    break;
                case Direction.Right:
                    yRotation += 1f;
                    if (yRotation >= _finRotation) _lockInput = false;
                    break;
            }
        }

        public override void UpdatePlayerMovement(Transform orientation, Rigidbody rb, float moveSpeed)
        {
            if (!IsKeyPressed && _lastDirection is Direction.Up or Direction.Down)
            {
                rb.velocity = Vector3.zero;
            }
            else if (_lastDirection is Direction.Up or Direction.Down)
            {
                _fractionWay += moveSpeed * 0.03f;
                transform.position = Vector3.Lerp(_startPos, _endPos, _fractionWay);
                if (CompareVector3(transform.position, _endPos)) _lockInput = false;
            }


            if (!_lockInput && (_keyForward && CheckForWallForward(rb, orientation) ||
                                _keyBack && CheckForWallBackward(rb, orientation)))
            {
                _lockInput = true;
                _lastDirection = _keyForward ? Direction.Up : Direction.Down;
                var position = rb.position;
                _startPos = position;
                _endPos = _lastDirection == Direction.Up
                    ? position + orientation.forward
                    : position - orientation.forward;
                _fractionWay = 0;
            }
        }

        public override void ReadPlayerMovement()
        {
            _keyForward = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
            _keyBack = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        }

        private bool CheckForWallForward(Rigidbody rb, Transform orientation)
        {
            var ray = new Ray(rb.position, orientation.forward);
            return !Physics.Raycast(ray, 1.2f);
        }

        private bool CheckForWallBackward(Rigidbody rb, Transform orientation)
        {
            var ray = new Ray(rb.position, -orientation.forward);
            return !Physics.Raycast(ray, 1.2f);
        }

        private bool IsKeyPressed => _keyForward || _keyBack || _lockInput;

        private static bool CompareVector3(Vector3 first, Vector3 second)
            => !(Math.Abs(first.x - second.x) > 0.005f) &&
               !(Math.Abs(first.y - second.y) > 0.005f) &&
               !(Math.Abs(first.z - second.z) > 0.005f);
    }
}