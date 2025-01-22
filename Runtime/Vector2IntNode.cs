using UnityEngine;

namespace SAMtak.AStar
{
    using INode = Algorithm<int>.INode;

    public abstract class Vector2IntNode : IntCostNode
    {
        public Vector2Int position;

        public override int EstimateCostTo(INode other) => Distance.Manhattan(position, ((Vector2IntNode)other).position);
    }
}