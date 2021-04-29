using System;
using Monolith.Tasks;
using UnityEngine.AddressableAssets;
using UnityObject = UnityEngine.Object;

namespace Monolith.Unity.Tasks
{
    
    public sealed class UnloadAssetTask<T> : ILazyTask where T : UnityObject
    {
        
        private readonly LoadAssetTask<T> _task;

        public UnloadAssetTask(LoadAssetTask<T> task)
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
        
    }
    
}