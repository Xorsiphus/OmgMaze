using UnityEngine;

namespace Entities
{
    public class MazeCell
    {
        public bool IsVisited;
        public bool IsFinish;
        private readonly int _x;
        private readonly int _y;

        public bool TopWall;
        public bool BottomWall;
        public bool LeftWall;
        public bool RightWall;

        public Vector2Int Position => new(_x, _y);

        public MazeCell(int x, int y)
        {
            _x = x;
            _y = y;

            IsVisited = false;

            TopWall = BottomWall = LeftWall = RightWall = true;
        }
    }
}