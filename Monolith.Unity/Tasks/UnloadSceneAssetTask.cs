using System;
using Monolith.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Monolith.Unity.Tasks
{
    
    public sealed class UnloadSceneAssetTask : ILazyTask
    {
        
        public AsyncOperationHandle<SceneInstance> Handle { get; private set; }
        
        private readonly LazySceneAssetTask _task;

        public UnloadSceneAssetTask(LazySceneAssetTask task) : base()
        {
            _task = task;
        }
        
        public void Start()
        {
            if (Handle.IsValid()) throw new InvalidOperationException();
            
            Handle = Addressables.UnloadSceneAsync(_task.Handle);
        }

        public bool IsDone => Handle.IsDone;
        public float Progress => Handle.PercentComplete;
        object ILazyTask.Result => null;
        
    }
    
}