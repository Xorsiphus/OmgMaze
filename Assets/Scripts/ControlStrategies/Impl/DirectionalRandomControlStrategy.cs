using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using Random = System.Random;

namespace ControlStrategies.Impl
{
    public sealed class DirectionalRandomControlStrategy : ControlStrategyAbstract
    {
        [SerializeField] private ControlStrategyAbstract directionalControlStrategy;

        private Direction? _direction;
        private Direction? _lastDirection;

        private void Update()
        {
            if (_lastDirection != Direction.Up) _direction = Direction.Up;
            else _direction ??= GetNextRandomDirection();
        }

        public override bool UpdateRotation(ref float xRotation, ref float yRotation)
        {
            if (_direction is null or Direction.Up) return true;
            switch (_direction)
            {
                case Direction.Left:
                    moveInputListener.EmulateLeftKey();
                    if (directionalControlStrategy.UpdateRotation(ref xRotation, ref yRotation)) ResetDirections();
                    break;
                case Direction.Right:
                    moveInputListener.EmulateRightKey();
                    if (directionalControlStrategy.UpdateRotation(ref xRotation, ref yRotation)) ResetDirections();
                    break;
                case Direction.Up:
                    break;
                case Direction.Down:
                    break;
                case null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return false;
        }

        public override void ReadPlayerMovement()
        {
        }

        public override bool UpdatePlayerMovement(Transform orientation, Rigidbody rb, float moveSpeed)
        {
            if (_direction is Direction.Up)
            {
                moveInputListener.EmulateForwardKey();
                if (directionalControlStrategy.UpdatePlayerMovement(orientation, rb, moveSpeed)) ResetDirections();
            }

            return true;
        }

        private void ResetDirections()
        {
            _lastDirection = _direction ?? Direction.Up;
            _direction = null;
        }

        private static Direction GetNextRandomDirection()
        {
            var directions = new List<Direction> { Direction.Left, Direction.Right };
            return directions[new Random().Next(0, directions.Count)];
        }

        private static Direction GetNextRandomDirection(IList<Direction> directions)
            => directions[new Random().Next(0, directions.Count)];
    }
}