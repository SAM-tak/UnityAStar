using System.Collections.Generic;

namespace SAMtak.AStar
{
    using INode = PathFinder<float>.INode;

    public abstract class FloatCostNode : INode
    {
        public float FinalCost => GraphCost + HeuristicCost;
        public float GraphCost { get; set; }
        public float HeuristicCost { get; set; }
        public INode Ancestor { get; set; }

        public bool IsCostLessThan(INode other)
        {
            return FinalCost < other.FinalCost || (FinalCost == other.FinalCost && HeuristicCost < other.HeuristicCost);
        }

        public bool IsMovementCostLessThanOthersGraphCost(INode other, out float movementCost)
        {
            movementCost = GraphCost + EstimateCostTo(other);
            return movementCost < other.GraphCost;
        }

        public void Reset()
        {
            GraphCost = 0f;
            HeuristicCost = 0f;
            Ancestor = null;
        }

        public abstract IEnumerable<INode> GetNeighbors();

        public abstract float EstimateCostTo(INode other);
    }
}