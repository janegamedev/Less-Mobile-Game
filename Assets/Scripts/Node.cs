using UnityEngine;

namespace LessBoardGame
{
   [System.Serializable]
   public class Node
   {
      public bool[] walls;
   }

   public enum Direction
   {
      N,
      E,
      S,
      W
   }
}
