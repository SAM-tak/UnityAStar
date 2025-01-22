using UnityEngine;

namespace SAMtak.AStar
{
    using INode = Algorithm<float>.INode;

    public abstract class Vector3Node : FloatCostNode
    {
        public Vector3Int position;

        public override float EstimateCostTo(INode other) => Vector3.Distance(position, ((Vector3Node)other).position);
    }
}