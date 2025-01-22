using UnityEngine;

namespace SAMtak.AStar
{
    using INode = Algorithm<float>.INode;

    public abstract class Vector2IntEuclidNode : FloatCostNode
    {
        public Vector2Int position;

        public override float EstimateCostTo(INode other) => Vector2.Distance(position, ((Vector2IntNode)other).position);
    }
}