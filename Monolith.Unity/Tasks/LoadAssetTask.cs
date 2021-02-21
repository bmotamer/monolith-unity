using System;
using Monolith.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Monolith.Unity.Tasks
{
    
    public sealed class LazyAssetTask<T> : ILazyTask
    {
        
        public AsyncOperationHandle<T> Handle { get; private set; }
        
        private readonly object _reference;

        public LazyAssetTask(object reference)
        {
            _reference = reference;
        }

        public LazyAssetTask(Func<object> func)
        {
            _reference = func;
        }
        
        public void Start()
        {
            if (Handle.IsValid()) throw new InvalidOperationException();

            if (_reference is Func<object> func)
            {
                Handle = Addressables.LoadAssetAsync<T>(func());
            }
            else
            {
                Handle = Addressables.LoadAssetAsync<T>(_reference);    
            }
        }

        public bool IsDone => Handle.IsDone;
        public float Progress => Handle.PercentComplete;
        public T Result => Handle.Result;
        object ILazyTask.Result => Result;
        
    }
    
}