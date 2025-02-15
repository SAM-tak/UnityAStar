using System.Collections.Generic;

namespace SAMtak.AStar
{
    using INode = PathFinder<float>.INode;

    /// <summary>
    /// Abstract node base class for float type cost path finder
    /// </summary>
    public abstract class FloatCostNode : INode
    {
        public float FinalCost => GraphCost + HeuristicCost;
        public float GraphCost { get; set; }
        public float HeuristicCost { get; set; }
        public INode Ancestor { get; set; }

        /// <inheritdoc/>
        public bool IsMovementCostLessThanNeighborsGraphCost(INode neighbor, out float movementCost)
        {
            movementCost = GraphCost + EstimateCostTo(neighbor);
            return movementCost < neighbor.GraphCost;
        }

        /// <inheritdoc/>
        public void Reset()
        {
            GraphCost = 0f;
            HeuristicCost = 0f;
            Ancestor = null;
        }

        public abstract IEnumerable<INode> GetNeighbors(INode goalNode);

        public abstract float EstimateCostTo(INode other);
    }
}