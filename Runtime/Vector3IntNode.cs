using UnityEngine;

namespace SAMtak.AStar
{
    using INode = PathFinder<int>.INode;

    /// <summary>
    /// IntCostNode for Vector3Int with manhattan distance
    /// </summary>
    public abstract class Vector3IntNode : IntCostNode
    {
        public Vector3Int position;

        public override int GetHashCode() => position.GetHashCode();

        public override int EstimateCostTo(INode other) => Distance.Manhattan(position, ((Vector3IntNode)other).position) + other.GraphCost;
    }
}