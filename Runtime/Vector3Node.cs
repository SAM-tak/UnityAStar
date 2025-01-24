using UnityEngine;

namespace SAMtak.AStar
{
    using INode = PathFinder<float>.INode;

    /// <summary>
    /// FloatCostNode for Vector3 (Euclid distance)
    /// </summary>
    public abstract class Vector3Node : FloatCostNode
    {
        public Vector3 position;

        public override int GetHashCode() => position.GetHashCode();

        public override float EstimateCostTo(INode other) => Vector3.Distance(position, ((Vector3Node)other).position);
    }
}
