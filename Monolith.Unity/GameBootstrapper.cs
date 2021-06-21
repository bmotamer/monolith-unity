using System;
using System.IO;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Monolith.Unity
{

    public static class GameBootstrapper
    {
        
        public static void Run<T>(Func<GameEngineListener, GameBootOptions, T> make) where T : Game
        {
            GameBootOptions bootOptions = null;
            
            var bootProfileSelector = Resources.Load<GameBootProfileSelector>("BootProfileSelector");
            
            if (bootProfileSelector != null)
            {
                if (bootProfileSelector.NeedsSanitizing)
                {
                    throw new InvalidDataException("Game could not be initialized because BootProfileSelector contains invalid data.");
                }
                else if (bootProfileSelector.NeedsSynchronization)
                {
                    Debug.LogWarning("Game may not behave as expected due to mismatched scripting define symbols!");
                }
                
                if (bootProfileSelector.ActiveIndex >= 0)
                {
                    bootOptions = (GameBootOptions)bootProfileSelector.Profiles[bootProfileSelector.ActiveIndex].BootOptions;
                }
            }

            Resources.UnloadAsset(bootProfileSelector);
            
            var gameObject = new GameObject("EngineListener")
            {
                hideFlags = HideFlags.HideAndDontSave
            };

            UnityObject.DontDestroyOnLoad(gameObject);

            var engineListener = gameObject.AddComponent<GameEngineListener>();

            make(engineListener, bootOptions);
        }

    }

}