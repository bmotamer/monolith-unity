using System;
using Monolith.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Monolith.Unity.Tasks
{
    
    public sealed class LoadSceneAssetTask : ILazyTask
    {
        
        public AsyncOperationHandle<SceneInstance> Handle { get; private set; }
        
        private readonly object _reference;

        public LoadSceneAssetTask(object reference)
        {
            _reference = reference;
        }
        
        public LoadSceneAssetTask(Func<object> reference)
        {
            _reference = reference;
        }
        
        public void Start()
        {
            if (Handle.IsValid()) throw new InvalidOperationException();

            if (_reference is Func<object> func)
            {
                Handle = Addressables.LoadSceneAsync(func());   
            }
            else
            {
                Handle = Addressables.LoadSceneAsync(_reference);    
            }
        }

        public bool IsDone => Handle.IsDone;
        public float Progress => Handle.PercentComplete;
        public SceneInstance Result => Handle.Result;
        object ILazyTask.Result => Result;
        
    }
    
}