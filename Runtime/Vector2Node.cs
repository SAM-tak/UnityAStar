using UnityEngine;

namespace SAMtak.AStar
{
    using INode = PathFinder<float>.INode;

    /// <summary>
    /// FloatCostNode for Vector2 (Euclid distance)
    /// </summary>
    public abstract class Vector2Node : FloatCostNode
    {
        public Vector2 position;

        public override int GetHashCode()
        {
            return position.GetHashCode();
        }

        public override float EstimateCostTo(INode other) => Vector2.Distance(position, ((Vector2Node)other).position);
    }
}