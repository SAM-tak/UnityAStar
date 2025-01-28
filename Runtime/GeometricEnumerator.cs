using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SAMtak.AStar
{
    /// <summary>
    /// Enumerator order by direction
    /// </summary>
    public static class GeometricEnumerator
    {
        /// <summary>
        /// Enumerate neighbors oreder by direction direction.
        /// </summary>
        /// <param name="current">Current position</param>
        /// <param name="neighbors">Neighbor nodes</param>
        /// <param name="direction">Base vector for sort</param>
        /// <returns>Sorted neighbors</returns>
        public static IEnumerable<IVector2Position> Enumerate(Vector2 current, IEnumerable<IVector2Position> neighbors, Vector2 direction)
        {
            foreach(var i in neighbors.OrderByDescending(x => Vector2.Dot((x.Position - current).normalized, direction))) {
                yield return i;
            }
        }

        /// <summary>
        /// Enumerate neighbors oreder by direction direction.
        /// </summary>
        /// <param name="current">Current position</param>
        /// <param name="neighbors">Neighbor nodes</param>
        /// <param name="direction">Base vector for sort</param>
        /// <returns>Sorted neighbors</returns>
        public static IEnumerable<IVector3Position> Enumerate(Vector3 current, IEnumerable<IVector3Position> neighbors, Vector3 direction)
        {
            foreach(var i in neighbors.OrderByDescending(x => Vector3.Dot((x.Position - current).normalized, direction))) {
                yield return i;
            }
        }
    }
}