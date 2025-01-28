using UnityEngine;

namespace SAMtak.AStar
{
    using INode = PathFinder<float>.INode;

    public interface IVector2Position
    {
        Vector2 Position { get; set; }
    }

    /// <summary>
    /// FloatCostNode for Vector2 (Euclid distance)
    /// </summary>
    public abstract class Vector2Node : FloatCostNode, IVector2Position
    {
        public Vector2 Position { get; set; }

        public override int GetHashCode() => Position.GetHashCode();

        /// <inheritdoc/>
        public override float EstimateCostTo(INode other) => Vector2.Distance(Position, ((IVector2Position)other).Position);
    }
}