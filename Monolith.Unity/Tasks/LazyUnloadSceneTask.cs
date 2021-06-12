using System;
using Monolith.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Monolith.Unity.Tasks
{
    
    public sealed class LazyUnloadSceneTask : ILazyTask
    {
        
        private readonly Scene _scene;
        private AsyncOperation _handle;

        public LazyUnloadSceneTask(Scene scene)
        {
            if (!scene.IsValid()) throw new ArgumentException();
            
            _scene = scene;
        }
        
        public bool IsDone => (_handle != null) && _handle.isDone;
        public float Progress => _handle == null ? 0.0F : _handle.progress;
        
        public void Start()
        {
            if (_handle != null) throw new InvalidOperationException();

            _handle = SceneManager.UnloadSceneAsync(_scene);
        }
        
        public void Update()
        {
        }

    }
    
}