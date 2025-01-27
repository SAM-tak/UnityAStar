namespace SAMtak.AStar
{
    using INode = PathFinder<int>.INode;

    /// <summary>
    /// IntCostNode for Vector2Int with manhattan distance
    /// </summary>
    public abstract class Vector2IntChebyshevNode : Vector2IntNode
    {
        /// <inheritdoc/>
        public override int EstimateCostTo(INode other) => Distance.Chebyshev(position, ((Vector2IntNode)other).position);
    }
}