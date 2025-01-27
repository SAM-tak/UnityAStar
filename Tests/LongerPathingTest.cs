using UnityEngine;
using NUnit.Framework;

namespace SAMtak.AStar.Tests
{
    [TestFixture]
    public class LongerPathingTests
    {
        private short[,] _grid;

        [SetUp]
        public void SetUp()
        {
            var level = @"################################
                          #000####00000000000000000000000#
                          #000####00000000000000000000000#
                          #000####00000000000000000000000#
                          #000####00000000000000000000000#
                          #000####00000000000000000000000#
                          #000####00000000000000000000000#
                          #000####00000000000000000000000#
                          #000000000000000000000000000000#
                          #000000000000000000000000000000#
                          #000000000000000000000000000000#
                          ######################000000000#
                          ######################000000000#
                          ######################000000000#
                          #0000000000000000#####000000000#
                          #0000000000000000#####000000000#
                          #0000000000000000#####000000000#
                          #0000000000000000#####000000000#
                          #0000000000000000#####000000000#
                          #0000000000000000#####000000000#
                          #0000000000000000#####000000000#
                          #0000000000000000#####000000000#
                          #0000000000000000#####000000000#
                          #0000000000000000#####000000000#
                          #0000000000000000#####000000000#
                          #0000000000000000#####000000000#
                          #000000000000000000000000000000#
                          #000000000000000000000000000000#
                          #00000##########################
                          #00000##########################
                          #000000000000000000000000000000#
                          ################################";

            _grid = Helper.ConvertStringToGrid(level);
        }

        [Test]
        public void ShouldPathEnvironment()
        {
            var pathfinder = new MockEuclidPathFinder(_grid);

            var path = pathfinder.FindPath(new Vector2Int(1, 1), new Vector2Int(30, 30));

            Helper.Print(_grid, path);

            Assert.That(path, Is.EquivalentTo(new[] {
                new Vector2Int(1, 1),
                new Vector2Int(2, 2),
                new Vector2Int(3, 3),
                new Vector2Int(3, 4),
                new Vector2Int(3, 5),
                new Vector2Int(3, 6),
                new Vector2Int(3, 7),
                new Vector2Int(4, 8),
                new Vector2Int(5, 9),
                new Vector2Int(6, 9),
                new Vector2Int(7, 9),
                new Vector2Int(8, 9),
                new Vector2Int(9, 9),
                new Vector2Int(10, 9),
                new Vector2Int(11, 9),
                new Vector2Int(12, 9),
                new Vector2Int(13, 10),
                new Vector2Int(14, 10),
                new Vector2Int(15, 10),
                new Vector2Int(16, 10),
                new Vector2Int(17, 10),
                new Vector2Int(18, 10),
                new Vector2Int(19, 10),
                new Vector2Int(20, 10),
                new Vector2Int(21, 10),
                new Vector2Int(22, 11),
                new Vector2Int(22, 12),
                new Vector2Int(22, 13),
                new Vector2Int(22, 14),
                new Vector2Int(22, 15),
                new Vector2Int(22, 16),
                new Vector2Int(22, 17),
                new Vector2Int(22, 18),
                new Vector2Int(22, 19),
                new Vector2Int(22, 20),
                new Vector2Int(22, 21),
                new Vector2Int(22, 22),
                new Vector2Int(22, 23),
                new Vector2Int(22, 24),
                new Vector2Int(22, 25),
                new Vector2Int(21, 26),
                new Vector2Int(20, 26),
                new Vector2Int(19, 26),
                new Vector2Int(18, 26),
                new Vector2Int(17, 26),
                new Vector2Int(16, 26),
                new Vector2Int(15, 26),
                new Vector2Int(14, 26),
                new Vector2Int(13, 26),
                new Vector2Int(12, 26),
                new Vector2Int(11, 26),
                new Vector2Int(10, 26),
                new Vector2Int(9, 26),
                new Vector2Int(8, 26),
                new Vector2Int(7, 26),
                new Vector2Int(6, 27),
                new Vector2Int(5, 28),
                new Vector2Int(5, 29),
                new Vector2Int(6, 30),
                new Vector2Int(7, 30),
                new Vector2Int(8, 30),
                new Vector2Int(9, 30),
                new Vector2Int(10, 30),
                new Vector2Int(11, 30),
                new Vector2Int(12, 30),
                new Vector2Int(13, 30),
                new Vector2Int(14, 30),
                new Vector2Int(15, 30),
                new Vector2Int(16, 30),
                new Vector2Int(17, 30),
                new Vector2Int(18, 30),
                new Vector2Int(19, 30),
                new Vector2Int(20, 30),
                new Vector2Int(21, 30),
                new Vector2Int(22, 30),
                new Vector2Int(23, 30),
                new Vector2Int(24, 30),
                new Vector2Int(25, 30),
                new Vector2Int(26, 30),
                new Vector2Int(27, 30),
                new Vector2Int(28, 30),
                new Vector2Int(29, 30),
                new Vector2Int(30, 30),
            }));
        }
    }
}