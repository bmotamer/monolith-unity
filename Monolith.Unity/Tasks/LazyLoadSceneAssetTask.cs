using System;
using Monolith.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Monolith.Unity.Tasks
{
    
    public sealed class LazyLoadSceneAssetTask : ILazyTask<SceneInstance>
    {

        internal AsyncOperationHandle<SceneInstance> Handle;
        
        private readonly Func<object> _reference;
        
        public LazyLoadSceneAssetTask(Func<object> reference)
        {
            _reference = reference;
        }
        
        public bool IsDone => Handle.IsDone;
        public float Progress => Handle.PercentComplete;
        public SceneInstance Result => Handle.Result;
        
        public void Start()
        {
            if (Handle.IsValid()) throw new InvalidOperationException();

            Handle = Addressables.LoadSceneAsync(_reference(), LoadSceneMode.Additive);
        }
        
        public void Update()
        {
        }

    }
    
}
