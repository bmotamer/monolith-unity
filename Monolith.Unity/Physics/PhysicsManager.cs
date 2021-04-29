using System;

namespace Monolith.Unity.Physics
{

    public abstract class PhysicsManager
    {

        private float _timeSinceLastSimulation;

        protected PhysicsManager()
        {
            _timeSinceLastSimulation = 0.0F;
        }

        public void AddElapsedTime(float deltaTime)
        {
            if (deltaTime < 0.0F) throw new ArgumentOutOfRangeException();

            checked { _timeSinceLastSimulation += deltaTime; }
        }

        public bool HasPendingSimulation(float fixedDeltaTime)
        {
            if (fixedDeltaTime < 0.0F) throw new ArgumentOutOfRangeException(nameof(fixedDeltaTime));

            return _timeSinceLastSimulation >= fixedDeltaTime;
        }

        public void Simulate(float fixedDeltaTime)
        {
            if (fixedDeltaTime < 0.0F) throw new ArgumentOutOfRangeException(nameof(fixedDeltaTime));
            if (_timeSinceLastSimulation < fixedDeltaTime) throw new ArgumentOutOfRangeException(nameof(fixedDeltaTime));

            _timeSinceLastSimulation -= fixedDeltaTime;

            SimulateInternal(fixedDeltaTime);
        }

        protected abstract void SimulateInternal(float fixedDeltaTime);

    }

}