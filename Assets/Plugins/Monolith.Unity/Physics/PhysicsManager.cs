using System;

namespace Monolith.Unity.Physics
{

    public abstract class PhysicsManager
    {

        private float _timeSinceLastStep;

        protected PhysicsManager()
        {
            _timeSinceLastStep = 0.0F;
        }

        public void AddElapsedTime(float deltaTime)
        {
            if (deltaTime < 0.0F) throw new ArgumentOutOfRangeException();

            checked { _timeSinceLastStep += deltaTime; }
        }

        public bool CanStep(float fixedDeltaTime)
        {
            if (fixedDeltaTime < 0.0F) throw new ArgumentOutOfRangeException(nameof(fixedDeltaTime));

            return _timeSinceLastStep >= fixedDeltaTime;
        }

        public void Step(float fixedDeltaTime)
        {
            if (fixedDeltaTime < 0.0F) throw new ArgumentOutOfRangeException(nameof(fixedDeltaTime));
            if (_timeSinceLastStep < fixedDeltaTime) throw new ArgumentOutOfRangeException(nameof(fixedDeltaTime));

            _timeSinceLastStep -= fixedDeltaTime;

            Simulate(fixedDeltaTime);
        }

        protected abstract void Simulate(float fixedDeltaTime);

    }

}