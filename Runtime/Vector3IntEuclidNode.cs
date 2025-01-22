using UnityEngine;

namespace SAMtak.AStar
{
    using INode = PathFinder<float>.INode;

    public abstract class Vector3IntEuclidNode : FloatCostNode
    {
        public Vector3Int position;

        public override float EstimateCostTo(INode other) => Vector3.Distance(position, ((Vector3IntNode)other).position);
    }
}