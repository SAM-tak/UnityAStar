using UnityEngine;
using NUnit.Framework;

namespace SAMtak.AStar.Tests
{
    [TestFixture]
    public class WeightedPathingTests
    {
        short[,] _grid;

        [SetUp]
        public void SetUp()
        {
            var level = @"1111115
                          1511151
                          1155511
                          1111111";

            _grid = Helper.ConvertStringToGrid(level);
        }

        [Test]
        public void ShouldPathWithWeight()
        {
            var pathfinder = new MockManhattanPathFinder(_grid);

            var path = pathfinder.FindPath(new Vector2Int(0, 0), new Vector2Int(6, 3));

            Helper.Print(_grid, path);

            Assert.That(path, Is.EquivalentTo(new[] {
                new Vector2Int(0, 0),
                new Vector2Int(0, 1),
                new Vector2Int(0, 2),
                new Vector2Int(0, 3),
                new Vector2Int(1, 3),
                new Vector2Int(2, 3),
                new Vector2Int(3, 3),
                new Vector2Int(4, 3),
                new Vector2Int(5, 3),
                new Vector2Int(6, 3),
            }));
        }

        [Test]
        public void ShouldPathWithWeightAlwaysConsiderGoalDirection()
        {
            var pathfinder = new MockManhattanPathFinderAlwaysConsiderGoal(_grid);

            var path = pathfinder.FindPath(new Vector2Int(0, 0), new Vector2Int(6, 3));

            Helper.Print(_grid, path);

            Assert.That(path, Is.EquivalentTo(new[] {
                new Vector2Int(0, 0),
                new Vector2Int(0, 1),
                new Vector2Int(0, 2),
                new Vector2Int(1, 2),
                new Vector2Int(1, 3),
                new Vector2Int(2, 3),
                new Vector2Int(3, 3),
                new Vector2Int(4, 3),
                new Vector2Int(5, 3),
                new Vector2Int(6, 3),
            }));
        }
    }
}