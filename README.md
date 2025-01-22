# UnityAStar

A A* algorithm path finding package for Unity

## Install

Open Package Manager window and press Add Package from git URL..., enter following path

```upm
https://github.com/SAM-tak/UnityAStar.git
```

or version specifying:

```upm
https://github.com/SAM-tak/UnityAStar.git#v0.5.0
```

## Example

```cs
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Customized path finder for your project.
public class MyPathFinder : SAMtak.AStar.Algorithm<int> // A* algorithm with int type cost
{
    public int Width { get; private set; }
    public int Height => _nodes.Length / Width;

    // A bidimentional array for grid world that obscured grid was filled by short.MaxValue.
    public short[,] Grid { get; private set; }

    public MyPathFinder(short[,] grid, int bufferReserveSize = 256) : base(bufferReserveSize)
    {
        var size = new Vector2Int(grid.GetLength(1), grid.GetLength(0));
        Grid = grid;
        Width = size.x;
        _nodes = new Node[size.x * size.y];
        for(int y = 0; y < size.y; ++y) {
            for(int x = 0; x < size.x; ++x) {
                var position = new Vector2Int(x, y);
                this[position] = new() {
                    position = position,
                    pathFinder = this
                };
            }
        }
    }

    public Vector2Int[] FindPath(Vector2Int start, Vector2Int goal)
    {
        return FindPath(this[start], this[goal]).Select(x => ((Node)x).position).ToArray();
    }

    public Node this[Vector2Int position] {
        get => _nodes[position.y * Width + position.x];
        set => _nodes[position.y * Width + position.x] = value;
    }

    readonly Node[] _nodes;

    protected override void OnStartFind()
    {
        for(int i = 0; i < _nodes.Length; ++i) {
            _nodes[i].Reset();
        }
    }

    // Vector2IntNode is abstract class designed for has int type cost, has Vector2Int position, has manhattan distance base heuristic cost.
    public class Node : SAMtak.AStar.Vector2IntNode
    {
        public MyPathFinder pathFinder;

        // customized heuristic cost function
        public override int EstimateCostTo(INode other)
        {
            var otherPosition = ((Node)other).position;
            return base.EstimateCostTo(other) + pathFinder.Grid[otherPosition.y, otherPosition.x]; // distance + weight
        }

        // returns neighbors from node graph. in this case, node graph was presented with 2D array.
        public override IEnumerable<INode> GetNeighbors()
        {
            if(position.x + 1 < pathFinder.Width && pathFinder.Grid[position.y, position.x + 1] < short.MaxValue) {
                yield return pathFinder[position + new Vector2Int(1, 0)];
            }
            if(position.y + 1 < pathFinder.Height && pathFinder.Grid[position.y + 1, position.x] < short.MaxValue) {
                yield return pathFinder[position + new Vector2Int(0, 1)];
            }
            if(position.y > 0 && pathFinder.Grid[position.y - 1, position.x] < short.MaxValue) {
                yield return pathFinder[position + new Vector2Int(0, -1)];
            }
            if(position.x > 0 && pathFinder.Grid[position.y, position.x - 1] < short.MaxValue) {
                yield return pathFinder[position + new Vector2Int(-1, 0)];
            }
        }
    }
}
```
