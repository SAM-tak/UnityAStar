using System.Collections.Generic;

namespace SAMtak.AStar
{
    using INode = Algorithm<int>.INode;

    public abstract class IntCostNode : INode
    {
        public int FinalCost => GraphCost + HeuristicCost;
        public int GraphCost { get; set; }
        public int HeuristicCost { get; set; }
        public INode Ancestor { get; set; }

        public bool IsCostLessThan(INode other)
        {
            return FinalCost < other.FinalCost || (FinalCost == other.FinalCost && HeuristicCost < other.HeuristicCost);
        }

        public bool IsMovementCostLessThanOthersGraphCost(INode other, out int movementCost)
        {
            movementCost = GraphCost + EstimateCostTo(other);
            return movementCost < other.GraphCost;
        }

        public void Reset()
        {
            GraphCost = 0;
            HeuristicCost = 0;
            Ancestor = null;
        }

        public abstract IEnumerable<INode> GetNeighbors();

        public abstract int EstimateCostTo(INode other);
    }
}