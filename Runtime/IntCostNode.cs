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
        public bool IsCostLessThan(INode other)
        {
            return FinalCost < other.FinalCost || (FinalCost == other.FinalCost && HeuristicCost < other.HeuristicCost);
        }

        /// <inheritdoc/>
        public bool IsMovementCostLessThanNeighborsGraphCost(INode neighbor, out int movementCost)
        {
            movementCost = GraphCost + EstimateCostTo(neighbor);
            return movementCost < neighbor.GraphCost;
        }

        /// <inheritdoc/>
        public void Reset(int initialCost)
        {
            GraphCost = initialCost;
            HeuristicCost = 0;
            Ancestor = null;
        }

        public abstract IEnumerable<INode> GetNeighbors();

        public abstract int EstimateCostTo(INode other);
    }
}