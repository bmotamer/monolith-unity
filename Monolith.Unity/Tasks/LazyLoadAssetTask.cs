using System;
using Monolith.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityObject = UnityEngine.Object;

namespace Monolith.Unity.Tasks
{
    
    public sealed class LazyLoadAssetTask<T> : ILazyTask<T> where T : UnityObject
    {

        internal AsyncOperationHandle<T> Handle;
        
        private readonly Func<object> _reference;

        public LazyLoadAssetTask(Func<object> reference)
        {
            _reference = reference;
        }
        
        public bool IsDone => Handle.IsDone;
        public float Progress => Handle.PercentComplete;
        public T Result => Handle.Result;
        
        public void Start()
        {
            if (Handle.IsValid()) throw new InvalidOperationException();

            Handle = Addressables.LoadAssetAsync<T>(_reference());
        }

        public void Update()
        {
        }

    }
    
}