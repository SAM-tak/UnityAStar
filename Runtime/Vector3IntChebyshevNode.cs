namespace SAMtak.AStar
{
    using INode = PathFinder<int>.INode;

    /// <summary>
    /// FloatCostNode for Vector2Int with euclid distance
    /// </summary>
    public abstract class Vector3IntChebyshevNode : Vector3IntNode
    {
        /// <inheritdoc/>
        public override int EstimateCostTo(INode other) => Distance.Chebyshev(position, ((Vector3IntNode)other).position);
    }
}