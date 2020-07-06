using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LessBoardGame
{
    public class Chip : MonoBehaviour, IMovable, IHaveState
    {
        [BoxGroup("Team")]
        public int teamNum;
        [BoxGroup("State")] 
        public ChipState currentState;
        
        private TileObject _currentTile;
        private TileObject _target;
        private float _moveCost;

        private List<TileObject> _path = new List<TileObject>();
        private List<TileObject> _pathForMovement = new List<TileObject>();

        private void Awake()
        {
            currentState = ChipState.UNSELECTED;
        }

        public TileObject[] ShowPossibleMoves()
        {
            throw new System.NotImplementedException();
        }

        public void Move(Vector3 target)
        {
            throw new System.NotImplementedException();
        }

        public ChipState GetState()
        {
            return currentState;
        }

        public void SetState(ChipState state)
        {
            currentState = state;
        }
    }

    public interface IMovable
    {
        TileObject[] ShowPossibleMoves();
        void Move(Vector3 target);
    }

    public interface IHaveState
    {
        ChipState GetState();
        void SetState(ChipState state);
    }

    public enum ChipState
    {
        UNSELECTED,
        SELECTED,
    }
}