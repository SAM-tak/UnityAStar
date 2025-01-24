using System.Linq;
using System.Text;
using UnityEngine;

namespace SAMtak.AStar.Tests
{
    public static class Helper
    {
        public const char ClosedCharacter = '#';

        public static void Print(short[,] grid, Vector2Int[] path)
        {
            var sb = new StringBuilder();
            // AppendGridString(sb, grid);
            // sb.AppendLine();
            AppendPathString(sb, grid, path);
            // sb.AppendLine();
            // AppendAssertionsString(sb, path);
            Debug.Log(sb.ToString());
        }

        public static short[,] ConvertStringToGrid(string level)
        {
            var splitLevel = level.Split('\n')
                .Select(row => row.Trim())
                .ToList();

            var grid = new short[splitLevel.Count, splitLevel[0].Length];

            for(var row = 0; row < splitLevel.Count; row++) {
                for(var column = 0; column < splitLevel[row].Length; column++) {
                    if(splitLevel[row][column] == ClosedCharacter) {
                        grid[row, column] = short.MaxValue;
                    }
                    else if(short.TryParse(splitLevel[row][column].ToString(), out var weight)) {
                        grid[row, column] = weight;
                    }
                    else {
                        grid[row, column] = 0;
                    }
                }
            }

            return grid;
        }

        static void AppendGridString(StringBuilder sb, short[,] grid, bool appendSpace = true)
        {
            for(var row = 0; row < grid.GetLength(0); row++) {
                for(var column = 0; column < grid.GetLength(1); column++) {
                    var value = grid[row, column];
                    if(value == short.MaxValue) sb.Append(ClosedCharacter);
                    else sb.Append(value);
                    if(appendSpace) {
                        sb.Append(' ');
                    }
                }
                sb.AppendLine();
            }
        }

        static void AppendPathString(StringBuilder sb, short[,] grid, Vector2Int[] path)
        {
            for(var row = 0; row < grid.GetLength(0); row++) {
                for(var column = 0; column < grid.GetLength(1); column++) {
                    if(path.Any(n => n.y == row && n.x == column)) {
                        sb.Append("*");
                    }
                    else {
                        var value = grid[row, column];
                        if(value == short.MaxValue) sb.Append(ClosedCharacter);
                        else sb.Append(value);
                    }
                    sb.Append(' ');
                }
                sb.AppendLine();
            }
        }

        static void AppendAssertionsString(StringBuilder sb, Vector2Int[] path)
        {
            sb.AppendLine("Assert.That(path, Is.EquivalentTo(new[] {");
            foreach(var position in path) {
                sb.AppendLine($"\tnew Vector2Int({position.x}, {position.y}),");
            }
            sb.AppendLine("}));");
        }
    }
}