using UnityEngine;

namespace SAMtak.AStar
{
    using INode = PathFinder<int>.INode;

    /// <summary>
    /// IntCostNode for Vector2Int with manhattan distance
    /// </summary>
    public abstract class Vector2IntNode : IntCostNode
    {
        public Vector2Int position;

        public override int GetHashCode()
        {
            return position.GetHashCode();
        }

        public override int EstimateCostTo(INode other) => Distance.Manhattan(position, ((Vector2IntNode)other).position);
    }
}