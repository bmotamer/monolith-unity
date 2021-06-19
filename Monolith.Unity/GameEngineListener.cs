using System;
using UnityEngine;

namespace Monolith.Unity
{

    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    public sealed class GameEngineListener : MonoBehaviour, IGameEngineListener
    {

        public event Action OnFrameEnter;
        public event Action OnFrameExit;
        public event Action OnDispose;

        private void Update()
        {
            OnFrameEnter?.Invoke();
        }

        private void LateUpdate()
        {
            OnFrameExit?.Invoke();
        }

        private void OnDestroy()
        {
            OnDispose?.Invoke();

            OnFrameEnter = null;
            OnFrameExit = null;
            OnDispose = null;
        }

    }

}