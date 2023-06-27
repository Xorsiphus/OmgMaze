using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField] private MazeGenerator mazeGenerator;
    [SerializeField] private GameObject mazeCellPrefab;

    public float cellSize = 1f;

    private void Start()
    {
        var maze = mazeGenerator.GetMaze();

        var runtimeTextureList = new List<Texture2D>();
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            var images = GetImagesInRuntime();
            if (images != null) runtimeTextureList.AddRange(images);
        }
        
        double sum = 0;
        var texList = new List<Tuple<Texture2D, double>>();
        var textures = Resources.LoadAll("Images").Cast<Texture2D>().ToArray();
        textures.AddRange(runtimeTextureList);
        foreach (var tex in textures)
        {
            sum += tex.name.Contains("base") ? 0.5 : 0.5 / (textures.Length - 1);
            texList.Add(new Tuple<Texture2D, double>(tex, sum));
        }

        for (var x = 0; x < mazeGenerator.mazeWidth; x++)
        {
            for (var y = 0; y < mazeGenerator.mazeHeight; y++)
            {
                var newCell = Instantiate(mazeCellPrefab,
                    new Vector3(x * cellSize, 0f, y * cellSize), Quaternion.identity, transform);

                for (var i = 1; i < 7; i++)
                {
                    var child = newCell.transform.GetChild(i);
                    var r = child.GetComponent<MeshRenderer>();
                    var rand = new System.Random().NextDouble();
                    var tex = texList.SkipWhile(t => t.Item2 < rand).First();
                    r.material.mainTexture = tex.Item1;
                }

                var mazeCell = newCell.GetComponent<MazeCellObject>();
                var top = maze[x, y].TopWall;
                var bottom = y == 0;
                var left = maze[x, y].LeftWall;
                var right = x == mazeGenerator.mazeWidth - 1;
                mazeCell.Init(top, bottom, left, right);
            }
        }
    }

    private static IEnumerable<Texture2D> GetImagesInRuntime()
    {
        if (!Directory.Exists($"{Application.dataPath}/Images")) return null;
        
        var files = Directory
            .GetFiles($"{Application.dataPath}/Images")
            .Where(f => !f.Contains(".meta"))
            .ToList();

        var texList = new List<Texture2D>();
        foreach (var stream in files.Select(filePath => new FileStream(filePath, FileMode.Open)))
        {
            byte[] image;
            using (var br = new BinaryReader(stream))
            {
                image = br.ReadBytes((int)stream.Length);
            }

            var tex = new Texture2D(1, 1);
            tex.LoadImage(image);
            texList.Add(tex);
        }

        return texList;
    }
}