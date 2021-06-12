using System;
using Monolith.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Monolith.Unity.Tasks
{
    
    public sealed class LazyUnloadSceneAssetTask : ILazyTask
    {

        internal AsyncOperationHandle<SceneInstance> Handle;
        
        private readonly LazyLoadSceneAssetTask _task;

        public LazyUnloadSceneAssetTask(LazyLoadSceneAssetTask task)
        {
            _task = task;
        }
        
        public bool IsDone => Handle.IsDone;
        public float Progress => Handle.PercentComplete;
        
        public void Start()
        {
            if (Handle.IsValid()) throw new InvalidOperationException();
            
            Handle = Addressables.UnloadSceneAsync(_task.Handle);
        }
        
        public void Update()
        {
        }

    }
    
}