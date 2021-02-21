using System;
using Monolith.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Monolith.Unity.Tasks
{
    
    public sealed class UnloadSceneTask : ILazyTask
    {
        
        public AsyncOperation Handle { get; private set; }
        
        private readonly Scene _scene;

        public UnloadSceneTask(Scene scene) : base()
        {
            if (!scene.IsValid()) throw new ArgumentException();
            
            _scene = scene;
        }
        
        public void Start()
        {
            if (Handle != null) throw new InvalidOperationException();

            Handle = SceneManager.UnloadSceneAsync(_scene);
        }

        public bool IsDone => Handle.isDone;
        public float Progress => Handle.progress;
        object ILazyTask.Result => null;
        
    }
    
}