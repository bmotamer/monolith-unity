using System;
using Monolith.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Monolith.Unity.Tasks
{
    
    public sealed class LoadSceneAssetTask : ILazyTask<SceneInstance>
    {

        internal AsyncOperationHandle<SceneInstance> Handle;
        
        private readonly Func<object> _reference;
        
        public LoadSceneAssetTask(Func<object> reference)
        {
            _reference = reference;
        }
        
        public void Start()
        {
            if (Handle.IsValid()) throw new InvalidOperationException();

            Handle = Addressables.LoadSceneAsync(_reference());
        }

        public bool IsDone => Handle.IsDone;
        public float Progress => Handle.PercentComplete;
        public SceneInstance Result => Handle.Result;
        
    }
    
}