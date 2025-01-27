using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SAMtak.AStar
{
    /// <summary>
    /// A* Algorithm path finder
    /// </summary>
    /// <typeparam name="T">Cost value type</typeparam>
    /// <example>
    /// // Customized path finder for your project.
    /// public class MyPathFinder : PathFinder<int> // A* algorithm path finder with int type cost
    /// {
    ///     public int Width { get; private set; }
    ///     public int Height => _nodes.Length / Width;
    ///     // A bidimentional array for grid world that obscured grid was filled by short.MaxValue.
    ///     public short[,] Grid { get; private set; }
    ///
    ///     public MyPathFinder(short[,] grid, int bufferReserveSize = 256) : base(bufferReserveSize)
    ///     {
    ///         var size = new Vector2Int(grid.GetLength(1), grid.GetLength(0));
    ///         Grid = grid;
    ///         Width = size.x;
    ///         _nodes = new Node[size.x * size.y];
    ///         for(int y = 0; y < size.y; ++y) {
    ///             for(int x = 0; x < size.x; ++x) {
    ///                 var position = new Vector2Int(x, y);
    ///                 this[position] = new() {
    ///                     position = position,
    ///                     pathFinder = this
    ///                 };
    ///             }
    ///         }
    ///     }
    ///
    ///     public Vector2Int[] FindPath(Vector2Int start, Vector2Int goal)
    ///     {
    ///         return FindPath(this[start], this[goal]).Select(x => ((Node)x).position).ToArray();
    ///     }
    ///
    ///     public Node this[Vector2Int position] {
    ///         get => _nodes[position.y * Width + position.x];
    ///         set => _nodes[position.y * Width + position.x] = value;
    ///     }
    ///
    ///     readonly Node[] _nodes;
    ///
    ///     protected override void OnStartFind()
    ///     {
    ///         for(int i = 0; i < _nodes.Length; ++i) {
    ///             _nodes[i].Reset();
    ///         }
    ///     }
    ///
    ///     public class Node : Vector2IntNode
    ///     {
    ///         public MyPathFinder pathFinder;
    ///         // customized heuristic cost function
    ///         public override int EstimateCostTo(INode other) => base.EstimateCostTo(other) + pathFinder.Grid[position.y, position.x]; // distance + weight
    ///         // returns neighbors from node graph. in this case, node graph was presented with 2D array.
    ///         public override IEnumerable<INode> GetNeighbors(INode goalNode)
    ///         {
    ///             foreach(var i in GridIterator.Manhattan(position, ((Node)goalNode).position)) {
    ///                 if(0 <= i.x && i.x < pathFinder.Width && 0 <= i.y && i.y < pathFinder.Height && pathFinder.Grid[i.y, i.x] < short.MaxValue) {
    ///                     yield return pathFinder[i];
    ///                 }
    ///             }
    ///         }
    ///     }
    /// }
    /// </example>
    public class PathFinder<T>
    {
        /// <summary>
        /// A* Algorithm node interface
        /// </summary>
        public interface INode
        {
            /// <summary>
            /// GraphCost + HeuristicCost
            /// </summary>
            /// <see cref="IntCostNode"/>
            /// <see cref="FloatCostNode"/>
            T FinalCost { get; }
            T GraphCost { get; set; }
            T HeuristicCost { get; set; }
            INode Ancestor { get; set; }
            /// <summary>
            /// Enumerate neighbor nodes
            /// </summary>
            /// <returns>The neighbors</returns>
            IEnumerable<INode> GetNeighbors(INode goalNode);
            /// <summary>
            /// Estimate cost for other
            /// </summary>
            /// <param name="other">target node for calculate cost</param>
            /// <returns>
            /// The estimated cost.</br>
            /// Typically, returns a distance of self node to other node.
            /// </returns>
            T EstimateCostTo(INode other);
            /// <summary>
            /// Condition for choose current node.
            /// </summary>
            /// <param name="other">comparee node</param>
            /// <returns>
            /// Returns true if FinalCost less than other.<br/>
            /// If FinalCost is same, returns true if HeuristicCost less than other.<br/>
            /// Otherwise returns false.
            /// </returns>
            /// <see cref="IntCostNode"/>
            /// <see cref="FloatCostNode"/>
            bool IsCostLessThan(INode other);
            /// <summary>
            /// Condition for update neighbor's graph cost.
            /// </summary>
            /// <param name="neighbor">neighbor node</param>
            /// <param name="movementCost">calculated new neighbor's graph cost</param>
            /// <returns>
            /// Set movementCost as self GraphCost + estimate cost to others.<br/>
            /// Retunrs true if movementCost less than neighbor's current graph cost.
            /// </returns>
            /// <see cref="IntCostNode"/>
            /// <see cref="FloatCostNode"/>
            bool IsMovementCostLessThanNeighborsGraphCost(INode neighbor, out T movementCost);
            /// <summary>
            /// Initialize node state.<br/>
            /// GraphCost set to initialCost, HeuristicCost set to Zero and Ancestor set to null.
            /// </summary>
            /// <param name="initialCost">Initial graph cost</param>
            void Reset();
        }

        /// <summary>
        /// Is already start path finding?
        /// </summary>
        public bool IsBusy { get; private set; }

        readonly List<INode> _openList;
        readonly HashSet<INode> _closedList;

        /// <summary>
        /// Create path finder
        /// </summary>
        /// <param name="bufferReserveSize">Reserve size for open and close list.</param>
        public PathFinder(int bufferReserveSize = 256)
        {
            _openList = new(bufferReserveSize);
            _closedList = new(bufferReserveSize);
        }

        /// <summary>
        /// Find shortest path
        /// </summary>
        /// <param name="startNode">A start node</param>
        /// <param name="goalNode">A goal node</param>
        /// <returns>The found path</returns>
        public IEnumerable<INode> FindPath(INode startNode, INode goalNode)
        {
            Debug.Assert(!IsBusy);
            IsBusy = true;
            OnStartFind();
            _openList.Clear();
            _closedList.Clear();

            _openList.Add(startNode);

            while(_openList.Count > 0) {
                var currentNode = _openList.First();

                foreach(var node in _openList.Skip(1)) {
                    if(node.IsCostLessThan(currentNode)) {
                        currentNode = node;
                    }
                }

                _openList.Remove(currentNode);
                _closedList.Add(currentNode);

                if(currentNode == goalNode) {
                    // reuse _openList as result list.
                    for(_openList.Clear(); currentNode != startNode; currentNode = currentNode.Ancestor) {
                        _openList.Add(currentNode);
                    }
                    _openList.Add(startNode);
                    IsBusy = false;
                    return _openList.AsEnumerable().Reverse();
                }

                foreach(var neighbor in currentNode.GetNeighbors(goalNode)) {
                    if(_closedList.Contains(neighbor)) {
                        continue;
                    }

                    if(currentNode.IsMovementCostLessThanNeighborsGraphCost(neighbor, out var movementCost) || !_openList.Contains(neighbor)) {
                        neighbor.GraphCost = movementCost;
                        neighbor.HeuristicCost = neighbor.EstimateCostTo(goalNode);
                        neighbor.Ancestor = currentNode;

                        if(!_openList.Contains(neighbor)) {
                            _openList.Add(neighbor);
                        }
                    }
                }
            }

            _openList.Clear();
            _closedList.Clear();
            IsBusy = false;
            return null;
        }

        /// <summary>
        /// Calls on start finding.<br/>
        /// Define a process all of nodes resets to initial state in inherited class.
        /// </summary>
        protected virtual void OnStartFind()
        {
        }
    }
}