using System.Collections.Generic;
using UnityEngine;

namespace SAMtak.AStar
{
    /// <summary>
    /// Distance functions
    /// </summary>
    public static class GridIterator
    {
        /// <summary>
        /// Enumerate four direction
        /// </summary>
        /// <param name="current">current position</param>
        /// <param name="goal">goal position</param>
        /// <returns>Enumerator for four direction</returns>
        public static IEnumerable<Vector2Int> Manhattan(Vector2Int current, Vector2Int goal)
        {
            return Manhattan(Vector2Int.zero, current, goal, false);
        }

        /// <summary>
        /// Enumerate four direction
        /// </summary>
        /// <param name="previous">ancestor node position</param>
        /// <param name="current">current position</param>
        /// <param name="goal">goal position</param>
        /// <returns>Enumerator for four direction</returns>
        public static IEnumerable<Vector2Int> Manhattan(Vector2Int previous, Vector2Int current, Vector2Int goal)
        {
            return Manhattan(previous, current, goal, true);
        }

        /// <summary>
        /// Enumerate eight direction
        /// </summary>
        /// <param name="previous">ancestor node position</param>
        /// <param name="current">current position</param>
        /// <param name="goal">goal position</param>
        /// <returns>Enumerator for four direction</returns>
        public static IEnumerable<Vector2Int> Chebyshev(Vector2Int current, Vector2Int goal)
        {
            return Chebyshev(Vector2Int.zero, current, goal, false);
        }

        /// <summary>
        /// Enumerate eight direction
        /// </summary>
        /// <param name="current">current position</param>
        /// <param name="goal">goal position</param>
        /// <returns>Enumerator for four direction</returns>
        public static IEnumerable<Vector2Int> Chebyshev(Vector2Int previous, Vector2Int current, Vector2Int goal)
        {
            return Chebyshev(previous, current, goal, true);
        }

        static IEnumerable<Vector2Int> Manhattan(Vector2Int previous, Vector2Int current, Vector2Int goal, bool isValidPrevious)
        {
            static Vector2Int Clamp(Vector2Int v) => v.x > v.y ? new(v.x < 0 ? -1 : 1, 0) : new(0, v.y < 0 ? -1 : 1);

            var direction = isValidPrevious ? Clamp(current - previous) : Clamp(goal - current);
            yield return current + direction;

            var perpendicular = Perpendicular(direction, (Vector2)goal - current);
            yield return current + perpendicular;
            yield return current - perpendicular;

            yield return current - direction;
        }

        static IEnumerable<Vector2Int> Chebyshev(Vector2Int previous, Vector2Int current, Vector2Int goal, bool isValidPrevious)
        {
            static Vector2Int Clamp(Vector2Int v) => new(v.x > 0 ? 1 : v.x < 0 ? -1 : 0, v.y > 0 ? 1 : v.y < 0 ? -1 : 0);

            var direction = isValidPrevious ? Clamp(current - previous) : Clamp(goal - current);
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
    }
}