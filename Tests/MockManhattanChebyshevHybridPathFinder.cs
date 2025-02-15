using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SAMtak.AStar.Tests
{
    public class MockManhattanChebyshevHybridPathFinder : PathFinder<int>
    {
        public int Width { get; private set; }
        public int Height => _nodes.Length / Width;
        public short[,] Grid { get; private set; }
        public bool IsDiagonal { get; private set; }

        public MockManhattanChebyshevHybridPathFinder(short[,] grid, int bufferReserveSize = 256) : base(bufferReserveSize)
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

        public Vector2Int[] FindPath(Vector2Int start, Vector2Int goal, bool isDiagonal)
        {
            IsDiagonal = isDiagonal;
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

        public class Node : Vector2IntNode
        {
            public MockManhattanChebyshevHybridPathFinder pathFinder;

            public override int EstimateCostTo(INode other)
            {
                if(pathFinder.IsDiagonal) {
                    return base.EstimateCostTo(other) + pathFinder.Grid[position.y, position.x];
                }
                else {
                    return Distance.Chebyshev(position, ((Node)other).position) + pathFinder.Grid[position.y, position.x];
                }
            }

            public override IEnumerable<INode> GetNeighbors(INode goalNode)
            {
                foreach(var i in pathFinder.IsDiagonal
                    ? GridEnumerator.OneManhattan(((Node)Ancestor)?.position ?? position, position, ((Node)goalNode).position)
                    : GridEnumerator.OneChebyshev(((Node)Ancestor)?.position ?? position, position, ((Node)goalNode).position)) {
                    if(0 <= i.x && i.x < pathFinder.Width && 0 <= i.y && i.y < pathFinder.Height && pathFinder.Grid[i.y, i.x] < short.MaxValue) {
                        yield return pathFinder[i];
                    }
                }
            }
        }
    }
}