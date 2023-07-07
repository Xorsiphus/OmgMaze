using UnityEngine;

namespace ControlStrategies.Impl
{
    public sealed class DirectionalAutoControlStrategy : ControlStrategyAbstract
    {
        [SerializeField] private ControlStrategyAbstract directionalControlStrategy;
        
        public override bool UpdateRotation(ref float xRotation, ref float yRotation)
        {
            moveInputListener.EmulateLeftKey();
            return true;
        }

        public override void ReadPlayerMovement()
        {
            throw new System.NotImplementedException();
        }

        public override bool UpdatePlayerMovement(Transform orientation, Rigidbody rb, float moveSpeed)
        {
            throw new System.NotImplementedException();
        }
    }
}