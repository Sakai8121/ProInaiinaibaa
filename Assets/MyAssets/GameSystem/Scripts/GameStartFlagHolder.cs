#nullable enable
using UnityEngine;
using VContainer.Unity;

namespace MyAssets.GameSystem.Scripts
{
    public class GameStartFlagHolder
    {
        public bool IsStartingGame { get; set; }

        public void StartGame()
        {
            IsStartingGame = true;
        }
    }
}