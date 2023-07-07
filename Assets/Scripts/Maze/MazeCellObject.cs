using UnityEngine;

namespace Maze
{
    public class MazeCellObject : MonoBehaviour
    {
        [SerializeField] private GameObject topWall;
        [SerializeField] private GameObject bottomWall;
        [SerializeField] private GameObject leftWall;
        [SerializeField] private GameObject rightWall;

        public void Init(bool top, bool bottom, bool left, bool right, bool isFinish)
        {
            topWall.SetActive(top);
            bottomWall.SetActive(bottom);
            leftWall.SetActive(left);
            rightWall.SetActive(right);

            var wallsSoundScript = gameObject.GetComponent<Transform>()
                .gameObject.GetComponentInChildren<PlaySoundOnMove>();

            if (isFinish) wallsSoundScript.isFinish = true;
        }
    }
}