using System;
using Monolith.Tasks;
using UnityEngine.AddressableAssets;

namespace Monolith.Unity.Tasks
{
    
    public sealed class UnloadAssetTask<T> : ILazyTask
    {
        
        private readonly LazyAssetTask<T> _task;

        public UnloadAssetTask(LazyAssetTask<T> task) : base()
        {
            _task = task;
        }
        
        public void Start()
        {
            if (IsDone) throw new InvalidOperationException();
            
            Addressables.Release(_task.Handle);

            IsDone = true;
        }

        public bool IsDone { get; private set; }

        public float Progress => IsDone ? 1F : 0F;
        object ILazyTask.Result => null;
        
    }
    
}