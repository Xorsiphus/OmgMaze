using UnityEngine;

namespace Entities
{
    public class MazeCell
    {
        public bool IsVisited;
        public bool IsFinish;
        public int X, Y;

        public bool TopWall;
        public bool BottomWall;
        public bool LeftWall;
        public bool RightWall;

        public Vector2Int Position => new(X, Y);

        public MazeCell(int x, int y)
        {
            X = x;
            Y = y;

            IsVisited = false;

            TopWall = BottomWall = LeftWall = RightWall = true;
        }
    }
}