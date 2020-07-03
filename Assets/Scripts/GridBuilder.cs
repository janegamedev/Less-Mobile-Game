using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LessBoardGame
{
    public class GridBuilder : MonoBehaviour
    {
        [BoxGroup("Grid Settings")]
        public Vector2Int gridSize;                  // Cards size of the grid 3 * 3
        [BoxGroup("Grid Settings")]
        public float cardSize;                       // How much nodes in the row are inside of a card 
        [BoxGroup("Cards")]
        public Card[] cards;
        [BoxGroup("Node")]
        public GameObject nodeObject;                // Prefab for node object
        [BoxGroup("Node")] 
        public Transform nodeRoot;
        [BoxGroup("Node")]
        public float nodeSize;                        // Full size of node

        
        private Vector2[,] _cardPositions; 
        private NodeObject[,] _nodeHolders;

        private void Awake()
        {
            // Initializing two arrays ona for holding cards center global positions and second one for all nodeHolders on the scene in proper grid order
            _nodeHolders = new NodeObject[gridSize.x * 2, gridSize.y * 2];
            _cardPositions = new Vector2[gridSize.x, gridSize.y];

            for (int y = 0; y < gridSize.y; y++)
            {
                for (int x = 0; x < gridSize.x; x++)
                {
                    Card current = cards[Random.Range(0, cards.Length)];    // Getting random card
                    
                    //Instantiating the card and receiving its nodes
                    Node[] nodes = InstantiateNode(current, new Vector2Int(x, y));
                    
                    for (int n = 0; n < nodes.Length; n++)                 
                    {
                        // Generating node position according to its card and location in it
                        Vector2Int nodePos = new Vector2Int();
                        
                        nodePos.y = Mathf.FloorToInt(n / 2);
                        nodePos.x = n % 2;
                        
                        // As nodes goes not usual row by row, but in the circle - shift in x position is required 
                        if (n == 2)
                            nodePos.x += 1;
                        if (n == 3)
                            nodePos.x -= 1;
                        
                        // Instantiating node holder to the scene to current node position
                        NodeObject node = Instantiate(nodeObject,
                            new Vector3(_cardPositions[x,y].x + (nodePos.x  * nodeSize - nodeSize / 2 ), _cardPositions[x,y].y - (nodePos.y  * nodeSize - nodeSize/2), transform.position.z),
                            Quaternion.identity, nodeRoot).GetComponent<NodeObject>();
                        node.gameObject.name = "Node :" + (nodePos.x  + x * 2) + " : " + (nodePos.y + y * 2);
                       
                        node.Init(nodes[n]);                                               // Init node holder with node data
                        node.neighbors = new NodeObject[4];                                // Init array of neighbors (4 directions)
                        
                        _nodeHolders[nodePos.x  + x * 2, nodePos.y + y * 2] = node;        // Add node to _nodesArray to its proper position on the grid
                    }
                }
            }

            // Setting up neighbors for all nodes
            PropagateNeighbors();
        }

        private void PropagateNeighbors()
        {
            // Propagate SetNeighbor for each node horizontally and vertically
            // Inside of SetNeighbor Node will call recursive SetNeighbor for its neighbor if it is nor null

            for (int y = 0; y < gridSize.y * 2; y++)
            {
                for (int x = 0; x < gridSize.x * 2; x++)
                {
                    Vector2Int dir = new Vector2Int(x, y);
                    
                    _nodeHolders[x, y].SetNeighbor(Direction.S, GetNeighbor(dir, Direction.S));
                    _nodeHolders[x, y].SetNeighbor(Direction.E, GetNeighbor(dir, Direction.E));
                }
            }
        }

        private NodeObject GetNeighbor(Vector2Int pos, Direction dir)
        {
            // Getting neighbor theoretical position
            Vector2Int nPos = GetNeighborPosition(pos, dir);
            
            Debug.Log("NOt sound yet");
            // Checking possibility to get a neighbor
            if(TryGetNeighbour(nPos) == false)
                return null;
            
            Debug.Log(_nodeHolders[nPos.x, nPos.y]);
            return _nodeHolders[nPos.x, nPos.y];
        }
        
        private bool TryGetNeighbour(Vector2Int pos)
        {
            return pos.x >= 0 && pos.x < gridSize.x * 2 && pos.y >= 0 && pos.y < gridSize.y * 2;
        }

        // Calculating neighbor position depends on origin point and direction
        private Vector2Int GetNeighborPosition(Vector2Int pos, Direction dir)
        {
            switch (dir)
            {
                case Direction.N:
                   return new Vector2Int(pos.x, pos.y - 1);
                case Direction.E:
                    return new Vector2Int(pos.x + 1, pos.y);
                case Direction.S:
                    return new Vector2Int(pos.x, pos.y + 1);
                case Direction.W:
                    return new Vector2Int(pos.x - 1, pos.y);
                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
            }
        }

        private Node[] InstantiateNode(Card card, Vector2Int position)
        { 
            int rotation = Random.Range(0, 4);            // Generate random rotation index

            Vector2 pos = new Vector3(transform.position.x + (position.x * cardSize),
                transform.position.y - (position.y * cardSize), transform.position.z);
            _cardPositions[position.x, position.y] = pos;
            
            GameObject go = Instantiate(card.prefab, pos, Quaternion.Euler(0, 0, -rotation * 90), transform);
            go.name = "Card" + position;
            
            // return rotated nodes from instantiated card
            return card.nodes.RotateNodes(rotation);
        } 
    }
}