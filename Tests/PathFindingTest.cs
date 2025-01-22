using UnityEngine;
using NUnit.Framework;

namespace SAMtak.AStar.Tests
{
    public class PathFindingTest
    {
        short[,] _grid;

        [SetUp]
        public void SetUp()
        {
            var level = @"#######
                          #00#00#
                          #00000#
                          #######";

            _grid = Helper.ConvertStringToGrid(level);
        }

        [Test]
        public void ShouldPathPredictablyManhattan()
        {
            var pathfinder = new MockManhattanPathFinder(_grid);

            var path = pathfinder.FindPath(new Vector2Int(1, 1), new Vector2Int(3, 2));

            // Helper.Print(_grid, path);

            Assert.That(path, Is.EquivalentTo(new[] {
                new Vector2Int(1, 1),
                new Vector2Int(2, 1),
                new Vector2Int(2, 2),
                new Vector2Int(3, 2),
            }));
        }

        [Test]
        public void ShouldPathPredictablyManhattan2()
        {
            var pathfinder = new MockManhattanPathFinder(_grid);

            var path = pathfinder.FindPath(new Vector2Int(1, 1), new Vector2Int(5, 1));

            // Helper.Print(_grid, path);

            Assert.That(path, Is.EquivalentTo(new[] {
                new Vector2Int(1, 1),
                new Vector2Int(2, 1),
                new Vector2Int(2, 2),
                new Vector2Int(3, 2),
                new Vector2Int(4, 2),
                new Vector2Int(5, 2),
                new Vector2Int(5, 1),
            }));
        }

        [Test]
        public void ShouldPathPredictablyChebyshev()
        {
            var pathfinder = new MockChebyshevPathFinder(_grid);

            var path = pathfinder.FindPath(new Vector2Int(1, 1), new Vector2Int(3, 2));

            // Helper.Print(_grid, path);

            Assert.That(path, Is.EquivalentTo(new[] {
                new Vector2Int(1, 1),
                new Vector2Int(2, 1),
                new Vector2Int(3, 2),
            }));
        }

        [Test]
        public void ShouldPathPredictablyChebyshev2()
        {
            var pathfinder = new MockChebyshevPathFinder(_grid);

            var path = pathfinder.FindPath(new Vector2Int(1, 1), new Vector2Int(5, 1));

            // Helper.Print(_grid, path);

            Assert.That(path, Is.EquivalentTo(new[] {
                new Vector2Int(1, 1),
                new Vector2Int(2, 1),
                new Vector2Int(3, 2),
                new Vector2Int(4, 1),
                new Vector2Int(5, 1),
            }));
        }

        [Test]
        public void ShouldPathPredictablyEuclid()
        {
            var pathfinder = new MockChebyshevPathFinder(_grid);

            var path = pathfinder.FindPath(new Vector2Int(1, 1), new Vector2Int(3, 2));

            // Helper.Print(_grid, path);

            Assert.That(path, Is.EquivalentTo(new[] {
                new Vector2Int(1, 1),
                new Vector2Int(2, 1),
                new Vector2Int(3, 2),
            }));
        }

        [Test]
        public void ShouldPathPredictablyEuclid2()
        {
            var pathfinder = new MockChebyshevPathFinder(_grid);

            var path = pathfinder.FindPath(new Vector2Int(1, 1), new Vector2Int(5, 1));

            // Helper.Print(_grid, path);

            Assert.That(path, Is.EquivalentTo(new[] {
                new Vector2Int(1, 1),
                new Vector2Int(2, 1),
                new Vector2Int(3, 2),
                new Vector2Int(4, 1),
                new Vector2Int(5, 1),
            }));
        }
    }
}
