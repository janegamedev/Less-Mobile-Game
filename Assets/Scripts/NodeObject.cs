using UnityEngine;

namespace LessBoardGame
{
    public class NodeObject : MonoBehaviour
    {
        public NodeObject[] neighbors;
        public bool[] walls;

        public void Init(Node node)
        {
            walls = node.walls;
        }
    }
}