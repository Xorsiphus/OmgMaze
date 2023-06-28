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
        
        private Direction _direction;
        private float _finRot;
        
        public override void UpdateRotation(ref float xRotation, ref float yRotation)
        {
            _keyLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
            _keyRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        
            if ((_keyLeft || _keyRight) && !_lockInput)
            {
                _lockInput = true;
                _finRot = _keyLeft ? yRotation - 90f : yRotation + 90f;
                _direction = _keyLeft ? Direction.Left : Direction.Right;
            }

            if (!_lockInput) return;
            switch (_direction)
            {
                case Direction.Left:
                    yRotation -= 0.5f;
                    if (yRotation <= _finRot) _lockInput = false;
                    break;
                case Direction.Right:
                    yRotation += 0.5f;
                    if (yRotation >= _finRot) _lockInput = false;
                    break;
            }
        }
        
        public override void UpdatePlayerMovement(Transform orientation, Rigidbody rb, float moveSpeed)
        {
            throw new NotImplementedException();
        }
        
        public override void ReadPlayerMovement()
        {
            throw new NotImplementedException();
        }
    }
}