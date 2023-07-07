using UnityEngine;

namespace InputControl
{
    public class MoveInputListener : MonoBehaviour
    {
        
        private bool _forwardFlag;
        private bool _backwardFlag;
        private bool _leftFlag;
        private bool _rightFlag;

        public bool CheckForwardControl() =>
            MarkFlag(ref _forwardFlag) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        public bool CheckBackwardControl() =>
            MarkFlag(ref _backwardFlag) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        public bool CheckLeftControl() =>
            MarkFlag(ref _leftFlag) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);

        public bool CheckRightControl() =>
            MarkFlag(ref _rightFlag) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        public void EmulateForwardKey() => _forwardFlag = true;
        public void EmulateBackwardKey() => _backwardFlag = true;
        public void EmulateLeftKey() => _leftFlag = true;
        public void EmulateRightKey() => _rightFlag = true;



        private static bool MarkFlag(ref bool flag)
        {
            if (!flag) return false;
            flag = false;
            return true;
        }
    }
}