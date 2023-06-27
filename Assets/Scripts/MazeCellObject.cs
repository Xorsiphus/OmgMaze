using UnityEngine;

public class MazeCellObject : MonoBehaviour
{
    [SerializeField] private GameObject topWallFront;
    [SerializeField] private GameObject topWallSide;
    [SerializeField] private GameObject bottomWall;
    [SerializeField] private GameObject leftWallFront;
    [SerializeField] private GameObject leftWallSide;
    [SerializeField] private GameObject rightWall;

    public void Init(bool top, bool bottom, bool left, bool right) 
    {
        topWallFront.SetActive(top);
        topWallSide.SetActive(top);
        bottomWall.SetActive(bottom);
        leftWallFront.SetActive(left);
        leftWallSide.SetActive(left);
        rightWall.SetActive(right);
    }
}
