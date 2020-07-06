using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LessBoardGame
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Scriptable Objects/ Card", order = 0)]
    public class Card : ScriptableObject
    {
        public GameObject prefab;
        public Tile[] nodes;
    }
}