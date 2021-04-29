using System;
using Monolith.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Monolith.Unity.Tasks
{
    
    public sealed class UnloadSceneTask : ILazyTask
    {
        
        private readonly Scene _scene;
        private AsyncOperation _handle;

        public UnloadSceneTask(Scene scene)
        {
            if (!scene.IsValid()) throw new ArgumentException();
            
            _scene = scene;
        }
        
        public void Start()
        {
            if (_handle != null) throw new InvalidOperationException();

            _handle = SceneManager.UnloadSceneAsync(_scene);
        }

        public bool IsDone => _handle.isDone;
        public float Progress => _handle.progress;
        
    }
    
}