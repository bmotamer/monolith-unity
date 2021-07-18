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

        private void TriggerFloat(float value) => OnTrigger?.Invoke(_animator, new AnimationEventArgument(value));

        private void TriggerInt(int value) => OnTrigger?.Invoke(_animator, new AnimationEventArgument(value));

        private void TriggerString(string value) => OnTrigger?.Invoke(_animator, new AnimationEventArgument(value));

        private void TriggerObject(Object value) => OnTrigger?.Invoke(_animator, new AnimationEventArgument(value));

        private void OnDestroy()
        {
            _animator = null;
            OnTrigger = null;
        }

    }

}
