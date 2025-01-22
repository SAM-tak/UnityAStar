using UnityEngine;

namespace SAMtak.AStar
{
    using INode = PathFinder<int>.INode;

    public abstract class Vector3IntNode : IntCostNode
    {
        public Vector3Int position;

        public override int EstimateCostTo(INode other) => Distance.Manhattan(position, ((Vector3IntNode)other).position);
    }
}