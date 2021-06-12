using System;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Monolith.Unity
{

    public static class UnityGameBootstrapper
    {
        
        public static void Run<T>(Func<UnityEngineListener, T> make) where T : Game
        {
            var gameObject = new GameObject("UnityEngineListener")
            {
                hideFlags = HideFlags.HideAndDontSave
            };

            UnityObject.DontDestroyOnLoad(gameObject);

            UnityEngineListener engineListener = gameObject.AddComponent<UnityEngineListener>();

            make(engineListener);
        }

    }

}