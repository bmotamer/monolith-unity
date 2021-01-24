using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Monolith.Unity.Extensions
{
    
    public static class SceneExtensions
    {

        private static readonly List<GameObject> _rootGameObjects;

        static SceneExtensions()
        {
            _rootGameObjects = new List<GameObject>(0);
        }
        
        public static T GetComponentInRootGameObjects<T>(this Scene scene) where T : Component
        {
            if (_rootGameObjects.Capacity < scene.rootCount)
            {
                _rootGameObjects.Capacity = scene.rootCount;
            }

            scene.GetRootGameObjects(_rootGameObjects);

            T component = null;

            foreach (GameObject rootGameObject in _rootGameObjects)
            {
                component = rootGameObject.GetComponent<T>();

                if (component) break;
            }
            
            _rootGameObjects.Clear();

            return component;
        }
        
    }
    
}