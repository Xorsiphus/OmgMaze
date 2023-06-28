using System.Collections.Generic;
using Entities;
using Enums;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [Range(5, 500)] public int mazeWidth = 5, mazeHeight = 5;

    public int startX, startY;
    private MazeCell[,] _maze;

    private Vector2Int _currentCell;

    private readonly List<Direction> _directions = new()
    {
        Direction.Up, Direction.Down, Direction.Left, Direction.Right
    };

    private List<Direction> GetRandomDirections()
    {
        var directions = new List<Direction>(_directions);
        var resultDirections = new List<Direction>();
        var rand = new System.Random();
        while (directions.Count > 0)
        {
            var rnd = rand.Next(directions.Count);
            resultDirections.Add(directions[rnd]);
            directions.RemoveAt(rnd);
        }

        return resultDirections;
    }

    private bool IsCellValid(int x, int y) =>
        !(x < 0 || y < 0 || x > mazeWidth - 1 || y > mazeHeight - 1 || _maze[x, y].IsVisited);

    private Vector2Int CheckNeighbours()
    {
        var directions = GetRandomDirections();

        foreach (var t in directions)
        {
            var neighbour = _currentCell;

            switch (t)
            {
                case Direction.Up:
                    neighbour.y++;
                    break;
                case Direction.Down:
                    neighbour.y--;
                    break;
                case Direction.Left:
                    neighbour.x--;
                    break;
                case Direction.Right:
                    neighbour.x++;
                    break;
            }

            if (IsCellValid(neighbour.x, neighbour.y)) return neighbour;
        }

        return _currentCell;
    }

    private void BreakWalls(Vector2Int primaryCell, Vector2Int secondaryCell)
    {
        if (primaryCell.x > secondaryCell.x)
        {
            _maze[primaryCell.x, primaryCell.y].LeftWall = false;
            _maze[secondaryCell.x, secondaryCell.y].RightWall = false;
        }
        else if (primaryCell.x < secondaryCell.x)
        {
            _maze[primaryCell.x, primaryCell.y].RightWall = false;
            _maze[secondaryCell.x, secondaryCell.y].LeftWall = false;
        }
        else if (primaryCell.y < secondaryCell.y)
        {
            _maze[primaryCell.x, primaryCell.y].TopWall = false;
            _maze[secondaryCell.x, secondaryCell.y].BottomWall = false;
        }
        else if (primaryCell.y > secondaryCell.y)
        {
            _maze[primaryCell.x, primaryCell.y].BottomWall = false;
            _maze[secondaryCell.x, secondaryCell.y].TopWall = false;
        }
    }

    private void CarvePath(int x, int y)
    {
        if (x < 0 || y < 0 || x > mazeWidth - 1 || y > mazeHeight - 1)
        {
            x = y = 0;
        }

        _currentCell = new Vector2Int(x, y);
        var path = new List<Vector2Int>();
        var finishCell = new Vector2Int();
        var deadEnd = false;
        while (!deadEnd)
        {
            var nextCell = CheckNeighbours();
            if (!_maze[nextCell.x, nextCell.y].IsVisited)
            {
                finishCell.x = nextCell.x;
                finishCell.y = nextCell.y;
            }
            
            _maze[_currentCell.x, _currentCell.y].IsVisited = true;

            if (nextCell == _currentCell)
            {
                for (var i = path.Count - 1; i >= 0; i--)
                {
                    _currentCell = path[i];
                    path.RemoveAt(i);
                    nextCell = CheckNeighbours();

                    if (nextCell != _currentCell) break;
                }

                if (nextCell == _currentCell) deadEnd = true;
            }
            else
            {
                BreakWalls(_currentCell, nextCell);
                _currentCell = nextCell;
                path.Add(_currentCell);
            }
        }

        _maze[finishCell.x, finishCell.y].IsFinish = true;
    }

    public MazeCell[,] GetMaze()
    {
        _maze = new MazeCell[mazeWidth, mazeHeight];

        for (var x = 0; x < mazeWidth; x++)
        {
            for (var y = 0; y < mazeHeight; y++)
            {
                _maze[x, y] = new MazeCell(x, y);
            }
        }

        CarvePath(startX, startY);

        return _maze;
    }
}

