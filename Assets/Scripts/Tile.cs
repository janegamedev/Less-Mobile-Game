using UnityEngine;

namespace LessBoardGame
{
   [System.Serializable]
   public class Tile
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
