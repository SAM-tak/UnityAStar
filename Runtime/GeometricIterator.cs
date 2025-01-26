using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SAMtak.AStar
{
    /// <summary>
    /// Enumerator order by direction
    /// </summary>
    public static class GeometricIterator
    {
        /// <summary>
        /// Enumerate oreder by goal direction.
        /// </summary>
        /// <param name="current">Current position</param>
        /// <param name="goal">The goal position</param>
        /// <param name="neighbors">Neighbor nodes</param>
        /// <returns>Sorted neighbors</returns>
        public static IEnumerable<Vector2> ConsiderGoal(Vector2 current, Vector2 goal, IEnumerable<Vector2> neighbors)
        {
            var direction = (goal - current).normalized;
            return Enumerate(current, neighbors, direction);
        }

        /// <summary>
        /// Enumerate neighbors oreder by current direction direction.
        /// </summary>
        /// <param name="previous">Previous position</param>
        /// <param name="current">Current position</param>
        /// <param name="neighbors">Neighbor nodes</param>
        /// <returns>Sorted neighbors</returns>
        public static IEnumerable<Vector2> ConsiderCurrentDirection(Vector2 previous, Vector2 current, IEnumerable<Vector2> neighbors)
        {
            var direction = (current - previous).normalized;
            return Enumerate(current, neighbors, direction);
        }

        static IEnumerable<Vector2> Enumerate(Vector2 current, IEnumerable<Vector2> neighbors, Vector2 direction)
        {
            foreach(var i in neighbors.OrderBy(x => Vector2.Dot((x - current).normalized, direction))) {
                yield return i;
            }
        }

        /// <summary>
        /// Enumerate oreder by goal direction.
        /// </summary>
        /// <param name="current">Current position</param>
        /// <param name="goal">The goal position</param>
        /// <param name="neighbors">Neighbor nodes</param>
        /// <returns>Sorted neighbors</returns>
        public static IEnumerable<Vector3> ConsiderGoal(Vector3 current, Vector3 goal, IEnumerable<Vector3> neighbors)
        {
            var direction = (goal - current).normalized;
            return Enumerate(current, neighbors, direction);
        }

        /// <summary>
        /// Enumerate neighbors oreder by current direction direction.
        /// </summary>
        /// <param name="previous">Previous position</param>
        /// <param name="current">Current position</param>
        /// <param name="neighbors">Neighbor nodes</param>
        /// <returns>Sorted neighbors</returns>
        public static IEnumerable<Vector3> ConsiderCurrentDirection(Vector3 previous, Vector3 current, IEnumerable<Vector3> neighbors)
        {
            var direction = (current - previous).normalized;
            return Enumerate(current, neighbors, direction);
        }

        static IEnumerable<Vector3> Enumerate(Vector3 current, IEnumerable<Vector3> neighbors, Vector3 direction)
        {
            foreach(var i in neighbors.OrderBy(x => Vector3.Dot((x - current).normalized, direction))) {
                yield return i;
            }
        }
    }
}