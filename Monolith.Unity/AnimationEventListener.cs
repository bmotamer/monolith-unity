using UnityEngine;

namespace Monolith.Unity.Utilities
{

    public delegate void AnimationEventListenerEvent(Animator animator, in AnimationEventArgument value);

    [RequireComponent(typeof(Animator)), DisallowMultipleComponent]
    public sealed class AnimationEventListener : MonoBehaviour
    {

        public event AnimationEventListenerEvent OnEvent;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Event() => OnEvent?.Invoke(_animator, default);

        private void Event(float value) => OnEvent?.Invoke(_animator, new AnimationEventArgument(value));

        private void Event(int value) => OnEvent?.Invoke(_animator, new AnimationEventArgument(value));

        private void Event(string value) => OnEvent?.Invoke(_animator, new AnimationEventArgument(value));

        private void Event(Object value) => OnEvent?.Invoke(_animator, new AnimationEventArgument(value));

        private void OnDestroy()
        {
            _animator = null;
        }

    }

}
