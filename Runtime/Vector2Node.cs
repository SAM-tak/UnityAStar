using UnityEngine;

namespace SAMtak.AStar
{
    using INode = PathFinder<float>.INode;

    public abstract class Vector2Node : FloatCostNode
    {
        public Vector2 position;

        public override float EstimateCostTo(INode other) => Vector2.Distance(position, ((Vector2Node)other).position);
    }
}