using UnityEngine;

namespace SAMtak.AStar
{
    public static class Distance
    {
        public static int Manhattan(Vector2Int a, Vector2Int b) => Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);

        public static int Manhattan(Vector3Int a, Vector3Int b) => Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z);

        public static int Chebyshev(Vector2Int a, Vector2Int b) => Mathf.Max(Mathf.Abs(a.x - b.x), Mathf.Abs(a.y - b.y));

        public static int Chebyshev(Vector3Int a, Vector3Int b) => Mathf.Max(Mathf.Max(Mathf.Abs(a.x - b.x), Mathf.Abs(a.y - b.y)), Mathf.Abs(a.z - b.z));
    }
}