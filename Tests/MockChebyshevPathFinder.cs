using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SAMtak.AStar.Tests
{
    public class MockChebyshevPathFinder : PathFinder<int>
    {
        public int Width { get; private set; }
        public int Height => _nodes.Length / Width;
        public short[,] Grid { get; private set; }

        public MockChebyshevPathFinder(short[,] grid, int bufferReserveSize = 256) : base(bufferReserveSize)
        {
            var size = new Vector2Int(grid.GetLength(1), grid.GetLength(0));
            Grid = grid;
            Width = size.x;
            _nodes = new Node[size.x * size.y];
            for(int y = 0; y < size.y; ++y) {
                for(int x = 0; x < size.x; ++x) {
                    var position = new Vector2Int(x, y);
                    this[position] = new() {
                        position = position,
                        pathFinder = this
                    };
                }
            }
        }

        public Vector2Int[] FindPath(Vector2Int start, Vector2Int goal)
        {
            return FindPath(this[start], this[goal]).Select(x => ((Node)x).position).ToArray();
        }

        public Node this[Vector2Int position] {
            get => _nodes[position.y * Width + position.x];
            set => _nodes[position.y * Width + position.x] = value;
        }

        readonly Node[] _nodes;

        protected override void OnStartFind()
        {
            for(int i = 0; i < _nodes.Length; ++i) {
                _nodes[i].Reset();
            }
        }

        public class Node : IntCostNode
        {
            public MockChebyshevPathFinder pathFinder;
            public Vector2Int position;

            public override int EstimateCostTo(INode other)
            {
                var otherPosition = ((Node)other).position;
                return Distance.Chebyshev(position, ((Node)other).position) + pathFinder.Grid[otherPosition.y, otherPosition.x];
            }

            public override IEnumerable<INode> GetNeighbors()
            {
                if(position.x + 1 < pathFinder.Width && pathFinder.Grid[position.y, position.x + 1] < short.MaxValue) {
                    yield return pathFinder[position + new Vector2Int(1, 0)];
                }
                if(position.x + 1 < pathFinder.Width && position.y + 1 < pathFinder.Height && pathFinder.Grid[position.y + 1, position.x + 1] < short.MaxValue) {
                    yield return pathFinder[position + new Vector2Int(1, 1)];
                }
                if(position.y + 1 < pathFinder.Height && pathFinder.Grid[position.y + 1, position.x] < short.MaxValue) {
                    yield return pathFinder[position + new Vector2Int(0, 1)];
                }
                if(position.x > 0 && position.y + 1 < pathFinder.Height && pathFinder.Grid[position.y + 1, position.x - 1] < short.MaxValue) {
                    yield return pathFinder[position + new Vector2Int(-1, 1)];
                }
                if(position.x > 0 && pathFinder.Grid[position.y, position.x - 1] < short.MaxValue) {
                    yield return pathFinder[position + new Vector2Int(-1, 0)];
                }
                if(position.x > 0 && position.y > 0 && pathFinder.Grid[position.y - 1, position.x - 1] < short.MaxValue) {
                    yield return pathFinder[position + new Vector2Int(-1, -1)];
                }
                if(position.y > 0 && pathFinder.Grid[position.y - 1, position.x] < short.MaxValue) {
                    yield return pathFinder[position + new Vector2Int(0, -1)];
                }
                if(position.x + 1 < pathFinder.Width && position.y > 0 && pathFinder.Grid[position.y - 1, position.x - 1] < short.MaxValue) {
                    yield return pathFinder[position + new Vector2Int(1, -1)];
                }
            }
        }
    }
}
