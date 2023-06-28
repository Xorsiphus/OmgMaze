using UnityEngine;

namespace ControlStrategies
{
    public class StandardFpControlStrategy : ControlStrategyAbstract
    {
        [SerializeField] private float sensX;
        [SerializeField] private float sensY;
        
        private float _horizontalInput;
        private float _verticalInput;
        private Vector3 _moveDirection;

        public override void UpdateRotation(ref float xRotation, ref float yRotation)
        {
            var mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * sensX;
            var mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * sensY;

            yRotation += mouseX;
            xRotation -= mouseY;
        }

        public override void UpdatePlayerMovement(Transform orientation, Rigidbody rb, float moveSpeed)
        {
            _moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;
            rb.AddForce(_moveDirection.normalized * (moveSpeed * 30f), ForceMode.Acceleration);
        }
        
        public override void ReadPlayerMovement()
        {
            _horizontalInput = Input.GetAxisRaw("Horizontal");
            _verticalInput = Input.GetAxisRaw("Vertical");
        }
    }
}