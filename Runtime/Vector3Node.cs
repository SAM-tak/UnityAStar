using UnityEngine;

namespace SAMtak.AStar
{
    using INode = PathFinder<float>.INode;

    public interface IVector3Position
    {
        Vector3 Position { get; set; }
    }

    /// <summary>
    /// FloatCostNode for Vector3 (Euclid distance)
    /// </summary>
    public abstract class Vector3Node : FloatCostNode, IVector3Position
    {
        public Vector3 Position { get; set; }

        public override int GetHashCode() => Position.GetHashCode();

        /// <inheritdoc/>
        public override float EstimateCostTo(INode other) => Vector3.Distance(Position, ((IVector3Position)other).Position);
    }
}