using UnityEngine;

namespace SAMtak.AStar
{
    using INode = PathFinder<float>.INode;

    /// <summary>
    /// FloatCostNode for Vector2Int with euclid distance
    /// </summary>
    public abstract class Vector2IntEuclidNode : FloatCostNode
    {
        public Vector2Int position;

        public override int GetHashCode() => position.GetHashCode();

        public override float EstimateCostTo(INode other) => Vector2.Distance(position, ((Vector2IntNode)other).position);
    }
}