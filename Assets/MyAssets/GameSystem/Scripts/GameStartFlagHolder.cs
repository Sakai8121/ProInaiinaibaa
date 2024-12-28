#nullable enable
using UnityEngine;
using VContainer.Unity;

namespace MyAssets.GameSystem.Scripts
{
    public class GameStartFlagHolder
    {
        public bool IsStartingGame { get; private set; }
        public bool IsEndGame { get;private set; }

        public void StartGame()
        {
            IsStartingGame = true;
        }

        public void EndGame()
        {
            IsEndGame = true;
        }
    }
}