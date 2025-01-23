using UnityEngine;
using NUnit.Framework;

namespace SAMtak.AStar.Tests
{
    [TestFixture]
    public class WeightedPathingTests
    {
        [Test]
        public void ShouldPathWithWeight()
        {
            var level = @"1111115
                          1511151
                          1155511
                          1111111";
            var world = Helper.ConvertStringToGrid(level);
            var pathfinder = new MockManhattanPathFinder(world);

            var path = pathfinder.FindPath(new Vector2Int(0, 0), new Vector2Int(6, 3));

            // Helper.Print(world, path);

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