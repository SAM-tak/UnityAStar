using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SAMtak.AStar
{
    /// <summary>
    /// Enumerator order by direction
    /// </summary>
    public static class GridEnumerator
    {
        /// <summary>
        /// Enumerate four direction
        /// </summary>
        /// <param name="previous">ancestor node position. if ancestor not exists, set current position.</param>
        /// <param name="current">current position</param>
        /// <param name="goal">goal position</param>
        /// <returns>Enumerator for four direction</returns>
        public static IEnumerable<Vector2Int> OneManhattan(Vector2Int previous, Vector2Int current, Vector2Int goal)
        {
            static Vector2Int Clamp(Vector2Int v) => v.x > v.y ? new(v.x < 0 ? -1 : 1, 0) : new(0, v.y < 0 ? -1 : 1);

            var direction = current - previous;
            if(direction == Vector2Int.zero) direction = Clamp(goal - current);
            else direction = Clamp(direction);
            yield return current + direction;

            var perpendicular = Perpendicular(direction, (Vector2)goal - current);
            yield return current + perpendicular;
            yield return current - perpendicular;

            yield return current - direction;
        }

        /// <summary>
        /// Enumerate eight direction
        /// </summary>
        /// <param name="previous">ancestor node position. if ancestor not exists, set current position.</param>
        /// <param name="current">current position</param>
        /// <param name="goal">goal position</param>
        /// <returns>Enumerator for eight direction</returns>
        public static IEnumerable<Vector2Int> OneChebyshev(Vector2Int previous, Vector2Int current, Vector2Int goal)
        {
            static Vector2Int Clamp(Vector2Int v) => new(v.x > 0 ? 1 : v.x < 0 ? -1 : 0, v.y > 0 ? 1 : v.y < 0 ? -1 : 0);

            var direction = current - previous;
            if(direction == Vector2Int.zero) direction = Clamp(goal - current);
            else direction = Clamp(direction);
            yield return current + direction;

            var perpendicular = Perpendicular(direction, (Vector2)goal - current);
            yield return current + Clamp(direction + perpendicular);
            yield return current + Clamp(direction - perpendicular);

            yield return current + perpendicular;
            yield return current - perpendicular;

            yield return current + Clamp(-direction + perpendicular);
            yield return current + Clamp(-direction - perpendicular);

            yield return current - direction;
        }

        static Vector2Int Perpendicular(Vector2Int direction, Vector2 goalDirection)
        {
            var perpendicularCCW = new Vector2Int(-direction.y, direction.x);
            var perpendicularCW = new Vector2Int(direction.y, -direction.x);
            return Vector2.Dot(goalDirection, perpendicularCCW) > Vector2.Dot(goalDirection, perpendicularCW) ? perpendicularCCW : perpendicularCW;
        }

        static readonly Vector3[] sixDirections = {
            Vector3.right, Vector3.left, Vector3.up, Vector3.down, Vector3.forward, Vector3.back
        };

        /// <summary>
        /// Enumerate six direction
        /// </summary>
        /// <param name="previous">ancestor node position</param>
        /// <param name="current">current position</param>
        /// <param name="goal">goal position</param>
        /// <returns>Enumerator for six direction</returns>
        public static IEnumerable<Vector3Int> OneManhattan(Vector3Int previous, Vector3Int current, Vector3Int goal)
        {
            var direction = ((Vector3)current - previous).normalized;
            if(direction == Vector3.zero) {
                direction = ((Vector3)goal - current).normalized;
            }
            return sixDirections.OrderByDescending(x => Vector3.Dot(x, direction)).Select(x => Vector3Int.CeilToInt(x));
        }

        static readonly Vector3[] fourteenDirections = {
            Vector3.right, Vector3.left, Vector3.up, Vector3.down, Vector3.forward, Vector3.back,
            Vector3.right + Vector3.up, Vector3.right + Vector3.down, Vector3.right + Vector3.forward, Vector3.right + Vector3.back,
            Vector3.left + Vector3.up, Vector3.left + Vector3.down, Vector3.left + Vector3.forward, Vector3.left + Vector3.back
        };

        /// <summary>
        /// Enumerate fourteen direction
        /// </summary>
        /// <param name="current">current position</param>
        /// <param name="goal">goal position</param>
        /// <returns>Enumerator for fourteen direction</returns>
        public static IEnumerable<Vector3Int> OneChebyshev(Vector3Int previous, Vector3Int current, Vector3Int goal)
        {
            var direction = ((Vector3)current - previous).normalized;
            if(direction == Vector3.zero) {
                direction = ((Vector3)goal - current).normalized;
            }
            return fourteenDirections.OrderByDescending(x => Vector3.Dot(x, direction)).Select(x => Vector3Int.CeilToInt(x));
        }
    }
}