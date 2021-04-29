using System;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Monolith.Unity
{

    public static class Bootstrapper
    {
        
        public static void Run<T>(Func<IEventListener, T> make) where T : Game
        {
            var gameObject = new GameObject("EventListener")
            {
                hideFlags = HideFlags.HideAndDontSave
            };

            UnityObject.DontDestroyOnLoad(gameObject);

            IEventListener eventListener = gameObject.AddComponent<EventListener>();

            make(eventListener);
        }

    }

}