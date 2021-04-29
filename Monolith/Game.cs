using Monolith.States;
using System;

namespace Monolith
{

    public abstract class Game
    {

        public readonly StateManager StateManager;

        private readonly IEventListener _eventListener;

        protected Game(IEventListener eventListener)
        {
            _eventListener = eventListener ?? throw new ArgumentNullException(nameof(eventListener));

            _eventListener.OnFrameEnter += Update;
            _eventListener.OnFrameExit += LateUpdate;
            _eventListener.OnDispose += Dispose;

            StateTreeStatic stateTree = CreateStateTree();

            StateManager = new StateManager(stateTree);
        }

        protected abstract StateTreeStatic CreateStateTree();

        private void Update()
        {
            StateManager.OnStartOfFrame(this);
            StateManager.OnUpdate(this);
        }

        private void LateUpdate()
        {
            StateManager.OnEndOfFrame(this);
        }

        private void Dispose()
        {
            _eventListener.OnFrameEnter -= Update;
            _eventListener.OnFrameExit -= LateUpdate;
            _eventListener.OnDispose -= Dispose;
        }

    }

}