using UnityEngine;

namespace ControlStrategies
{
    public abstract class ControlStrategyAbstract : MonoBehaviour
    {
        public abstract void UpdateRotation(ref float xRotation, ref float yRotation);
        public abstract void ReadPlayerMovement();
        public abstract void UpdatePlayerMovement(Transform orientation, Rigidbody rb, float moveSpeed);
    }
}