using System;
using Monolith.Tasks;
using UnityEngine.AddressableAssets;
using UnityObject = UnityEngine.Object;

namespace Monolith.Unity.Tasks
{
    
    public sealed class LazyUnloadAssetTask<T> : ILazyTask where T : UnityObject
    {
        
        private readonly LazyLoadAssetTask<T> _task;

        public LazyUnloadAssetTask(LazyLoadAssetTask<T> task)
        {
            _task = task;
        }
        
        public bool IsDone { get; private set; }
        public float Progress => IsDone ? 1F : 0F;
        
        public void Start()
        {
            if (IsDone) throw new InvalidOperationException();
            
            Addressables.Release(_task.Handle);

            IsDone = true;
        }
        
        public void Update()
        {
        }

    }
    
}