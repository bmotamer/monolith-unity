using UnityEngine;

namespace Monolith.Unity
{

    public delegate void AnimationEventListenerTrigger(Animator animator, in AnimationEventArgument arg);

    [RequireComponent(typeof(Animator)), DisallowMultipleComponent]
    public sealed class AnimationEventListener : MonoBehaviour
    {

        public event AnimationEventListenerTrigger OnTrigger;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Trigger() => OnTrigger?.Invoke(_animator, default);

        private void Trigger(float value) => OnTrigger?.Invoke(_animator, new AnimationEventArgument(value));

        private void Trigger(int value) => OnTrigger?.Invoke(_animator, new AnimationEventArgument(value));

        private void Trigger(string value) => OnTrigger?.Invoke(_animator, new AnimationEventArgument(value));

        private void Trigger(Object value) => OnTrigger?.Invoke(_animator, new AnimationEventArgument(value));

        private void OnDestroy()
        {
            _animator = null;
            OnTrigger = null;
        }

    }

}
