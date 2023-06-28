using System.Linq;
using Enums;
using UnityEngine;

public class MazeCellObject : MonoBehaviour
{
    [SerializeField] private GameObject topWall;
    [SerializeField] private GameObject bottomWall;
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;

    public void Init(bool top, bool bottom, bool left, bool right, int finishWallNum)
    {
        topWall.SetActive(top);
        bottomWall.SetActive(bottom);
        leftWall.SetActive(left);
        rightWall.SetActive(right);
        
        var wallsSoundScript = gameObject.GetComponent<Transform>()
            .gameObject.GetComponentsInChildren<PlaySoundOnMove>()
            .ToList();

        PlaySoundOnMove script;
        switch (finishWallNum)
        {
            case 2:
                script = wallsSoundScript.SingleOrDefault(s => s.name.Equals(nameof(WallNames.BottomWall)));
                if (script != null) script.isFinish = true;
                break;
            case 1:
                script = wallsSoundScript.SingleOrDefault(s => s.name.Equals(nameof(WallNames.TopWall)));
                if (script != null) script.isFinish = true;
                break;
            case 3:
                script = wallsSoundScript.SingleOrDefault(s => s.name.Equals(nameof(WallNames.RightWall)));
                if (script != null) script.isFinish = true;
                break;
            case 4:
                script = wallsSoundScript.SingleOrDefault(s => s.name.Equals(nameof(WallNames.LeftWall)));
                if (script != null) script.isFinish = true;
                break;
            default:
                return;
        }
    }
}