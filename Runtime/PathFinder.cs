using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SAMtak.AStar
{
    /// <summary>
    /// A* Algorithm
    /// </summary>
    /// <typeparam name="T">Cost value type</typeparam>
    public class PathFinder<T>
    {
        public interface INode
        {
            T FinalCost { get; }
            T GraphCost { get; set; }
            T HeuristicCost { get; set; }
            INode Ancestor { get; set; }
            IEnumerable<INode> GetNeighbors();
            bool IsMovementCostLessThanOthersGraphCost(INode other, out T movementCost);
            T EstimateCostTo(INode other);
            bool IsCostLessThan(INode other);
            void Reset();
        }

        public bool IsBusy { get;  private set; }

        readonly List<INode> _openList;
        readonly HashSet<INode> _closedList;

        public PathFinder(int bufferReserveSize = 256)
        {
            _openList = new(bufferReserveSize);
            _closedList = new(bufferReserveSize);
        }

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

                foreach(var node in _openList) {
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

                foreach(var i in currentNode.GetNeighbors()) {
                    if(_closedList.Contains(i)) {
                        continue;
                    }

                    if(currentNode.IsMovementCostLessThanOthersGraphCost(i, out var movementCost) || !_openList.Contains(i)) {
                        i.GraphCost = movementCost;
                        i.HeuristicCost = i.EstimateCostTo(goalNode);
                        i.Ancestor = currentNode;

                        if(!_openList.Contains(i)) {
                            _openList.Add(i);
                        }
                    }
                }
            }

            _openList.Clear();
            _closedList.Clear();
            IsBusy = false;
            return null;
        }

        protected virtual void OnStartFind()
        {
        }
    }
}