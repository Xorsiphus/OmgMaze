using System;
using Enums;
using UnityEngine;

namespace ControlStrategies.Impl
{
    public class DirectionalControlStrategy : ControlStrategyAbstract
    {
        private bool _lockInput;
        private bool _keyLeft;
        private bool _keyRight;
        private bool _keyForward;
        private bool _keyBackward;
        private Direction _lastDirection;

        private Vector3 _moveDirection;
        private Vector3 _startPos;
        private Vector3 _endPos;
        private float _fractionWay;

        private float _finRotation;

        public override bool UpdateRotation(ref float xRotation, ref float yRotation)
        {
            _keyLeft = moveInputListener.CheckLeftControl();
            _keyRight = moveInputListener.CheckRightControl();

            if ((_keyLeft || _keyRight) && !_lockInput)
            {
                _lockInput = true;
                _finRotation = _keyLeft ? yRotation - 90f : yRotation + 90f;
                _lastDirection = _keyLeft ? Direction.Left : Direction.Right;
            }

            if (!_lockInput) return true;
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
                case Direction.Up:
                    break;
                case Direction.Down:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return Math.Abs(yRotation - _finRotation) < 0.01;
        }

        public override bool UpdatePlayerMovement(Transform orientation, Rigidbody rb, float moveSpeed)
        {
            var isDone = false;
            _keyForward = moveInputListener.CheckForwardControl();
            _keyBackward = moveInputListener.CheckBackwardControl();

            if (!_lockInput && (_keyForward && CheckForWallForward(rb, orientation) ||
                                _keyBackward && CheckForWallBackward(rb, orientation)))
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
            else isDone = true;
            
            if (!IsKeyPressed)
            {
                rb.velocity = Vector3.zero;
            }
            else if (_lastDirection is Direction.Up or Direction.Down)
            {
                _fractionWay += moveSpeed * 0.03f;
                transform.position = Vector3.Lerp(_startPos, _endPos, _fractionWay);
                if (CompareVector3(transform.position, _endPos))
                {
                    _lockInput = false;
                    isDone = true;
                }
            }

            return isDone;
        }

        public override void ReadPlayerMovement()
        {
        }

        private bool IsKeyPressed => _keyForward || _keyBackward || _lockInput;

        private static bool CompareVector3(Vector3 first, Vector3 second)
            => !(Math.Abs(first.x - second.x) > 0.005f) &&
               !(Math.Abs(first.y - second.y) > 0.005f) &&
               !(Math.Abs(first.z - second.z) > 0.005f);
    }
}