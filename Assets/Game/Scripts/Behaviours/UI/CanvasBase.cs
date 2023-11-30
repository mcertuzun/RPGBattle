using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Behaviours.UI
{
    public class GameStateManager
    {
        private static GameStateManager _gameStateManager;
        [SerializeField] private GameState currentState;
        public static event Action<GameState> OnStateChange;

        public static GameState GetState()
        {
            if (_gameStateManager == null)
            {
                _gameStateManager = new GameStateManager();
            }

            return _gameStateManager.currentState;
        }

        public static void SetState(GameState gameState)
        {
            if (_gameStateManager == null)
            {
                _gameStateManager = new GameStateManager();
            }

            _gameStateManager.currentState = gameState;
            OnStateChange?.Invoke(gameState);
        }
    }

    public interface IActivator
    {
        void SetActivate(bool val);
    }

    public interface IStateListener
    {
        void StartListen();
        void StopListen();
        void OnStateChange(GameState gameState);
    }

    public abstract class CanvasBase : MonoBehaviour, IStateListener, IActivator
    {
        public StateMask stateMask;

        public void StartListen()
        {
            GameStateManager.OnStateChange += OnStateChange;
            OnStateChange(GameStateManager.GetState());
        }

        public void StopListen()
        {
            GameStateManager.OnStateChange -= OnStateChange;
        }

        public void OnStateChange(GameState gameState)
        {
            SetActivate(stateMask.GamesStateContains(gameState));
        }

        public void SetActivate(bool val)
        {
            gameObject.SetActive(val);
        }

        protected virtual void OnDestroy()
        {
            StopListen();
        }
    }

    [Serializable]
    public class StateMask
    {
        public List<GameState> gameStates = new();

        public bool GamesStateContains(GameState gameState)
        {
            return gameStates.Contains(gameState);
        }
    }

    public enum GameState
    {
        InGameUI,
        VictoryUI,
        DefeatUI
    }
}