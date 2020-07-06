using Sirenix.OdinInspector;
using UnityEngine;

namespace LessBoardGame
{
    public class TileObject : MonoBehaviour
    {
        [BoxGroup("Node surrounding")]
        public TileObject[] neighbors;
        [BoxGroup("Node surrounding")]
        public bool[] walls;
        
        [BoxGroup("Node setting")]
        public bool walkable;                          // If this node is occupated it becomes unwalkable
        [BoxGroup("Node setting")]
        public const int MOVEMENT_COST = 1;            // The cost of movement to this node without any additional chrages
        
        public void Init(Tile tile)
        {
            walls = tile.walls;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
    
    
}