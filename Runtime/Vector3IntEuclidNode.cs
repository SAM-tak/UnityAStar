using UnityEngine;

namespace SAMtak.AStar
{
    using INode = PathFinder<float>.INode;

    /// <summary>
    /// FloatCostNode for Vector3Int with euclid distance
    /// </summary>
    public abstract class Vector3IntEuclidNode : FloatCostNode
    {
        public Vector3Int position;

        public override int GetHashCode() => position.GetHashCode();

        /// <inheritdoc/>
        public override float EstimateCostTo(INode other) => Vector3.Distance(position, ((Vector3IntNode)other).position);
    }
}