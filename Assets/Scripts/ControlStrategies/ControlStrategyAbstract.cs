using InputControl;
using UnityEngine;

namespace ControlStrategies
{
    public abstract class ControlStrategyAbstract : MonoBehaviour
    {
        [SerializeField] protected MoveInputListener moveInputListener;

        public abstract bool UpdateRotation(ref float xRotation, ref float yRotation);
        public abstract void ReadPlayerMovement();
        public abstract bool UpdatePlayerMovement(Transform orientation, Rigidbody rb, float moveSpeed);
        
        protected static bool CheckForWallForward(Rigidbody rb, Transform orientation)
        {
            var ray = new Ray(rb.position, orientation.forward);
            return !Physics.Raycast(ray, 0.8f);
        }

        protected static bool CheckForWallBackward(Rigidbody rb, Transform orientation)
        {
            var ray = new Ray(rb.position, -orientation.forward);
            return !Physics.Raycast(ray, 0.8f);
        }
    }
}