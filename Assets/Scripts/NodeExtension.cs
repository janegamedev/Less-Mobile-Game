using System;
using UnityEngine;

namespace LessBoardGame
{
    public static class NodeExtension
    {
        // If a card will be rotated we need to rotate nodes too
        // Rotation of nodes will check for every wall in each node and change its direction and node position according to rotation int
        public static Tile[] RotateNodes(this Tile[] nodes, int rotate)
        {
            if (rotate == 0) return nodes;        // If rotation is 0 then card has its origin rotation and next node rotation can be skipped

            Tile[] copy = new Tile[4]
            {
                new Tile(){walls = new bool[4]},
                new Tile(){walls = new bool[4]},
                new Tile(){walls = new bool[4]},
                new Tile(){walls = new bool[4]},
            };
            
            for (int n = 0; n < nodes.Length; n++)
            {
                for (int w = 0; w < nodes[n].walls.Length; w++)
                {
                    if (nodes[n].walls[w] == true)
                    {
                        // Getting the initial position of the node and wall
                        int nextN = n;
                        int nextW = w;

                        // Iterate over increasing node index and wall index 
                        for (int i = 0; i < rotate; i++)
                        {
                            nextN = nextN + 1 < copy.Length ? nextN + 1 : 0;
                            nextW = nextW + 1 < copy[n].walls.Length ? nextW + 1 : 0;
                        }

                        // Setting the wall to true
                        copy[nextN].walls[nextW] = true;
                    }
                }
            }

            return copy;
        }

        public static TileObject GetNeighbor(this TileObject tile, Direction dir)
        {
            return tile.neighbors[(int)dir];
        }

        //Set given neighbor accordingly to its direction and propagate set neighbor to it if it is still null 
        public static void SetNeighbor(this TileObject tile, Direction dir, TileObject neighbor)
        {
            tile.neighbors[(int) dir] = neighbor;
            
            if(neighbor!= null && neighbor.GetNeighbor(dir.Opposite()) == null)
                neighbor.SetNeighbor(dir.Opposite(), tile);
        }

        private static Direction Opposite(this Direction dir)
        {
            switch (dir)
            {
                case Direction.N:
                    return Direction.S;
                case Direction.E:
                    return Direction.W;
                case Direction.S:
                    return Direction.N;
                case Direction.W:
                    return Direction.E;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
            }
        } 
    }
}