using System.Collections.Generic;

namespace SAMtak.AStar
{
    using INode = PathFinder<int>.INode;

    /// <summary>
    /// Abstract node base class for int type cost path finder
    /// </summary>
    public abstract class IntCostNode : INode
    {
        public int FinalCost => GraphCost + HeuristicCost;
        public int GraphCost { get; set; }
        public int HeuristicCost { get; set; }
        public INode Ancestor { get; set; }

        /// <inheritdoc/>
        public bool IsMovementCostLessThanNeighborsGraphCost(INode neighbor, out int movementCost)
        {
            movementCost = GraphCost + EstimateCostTo(neighbor);
            return movementCost < neighbor.GraphCost;
        }

        /// <inheritdoc/>
        public void Reset()
        {
            GraphCost = 0;
            HeuristicCost = 0;
            Ancestor = null;
        }

        /// <inheritdoc/>
        public abstract IEnumerable<INode> GetNeighbors(INode goalNode);

        /// <inheritdoc/>
        public abstract int EstimateCostTo(INode other);
    }
}