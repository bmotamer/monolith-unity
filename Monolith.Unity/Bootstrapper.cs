using Monolith;
using System;
using UnityEngine;

using UnityObject = UnityEngine.Object;

namespace Monolith.Unity
{

    public static class Bootstrapper
    {

        public static T Run<T>() where T : Game
        {
            GameObject gameObject = new GameObject("EventListener");
            gameObject.hideFlags = HideFlags.HideAndDontSave;

            UnityObject.DontDestroyOnLoad(gameObject);

            IEventListener eventListener = gameObject.AddComponent<EventListener>();

            return (T)Activator.CreateInstance(typeof(T), eventListener);
        }

    }

}