using Monolith.States;
using System;

namespace Monolith
{

    public abstract class Game
    {

        public readonly GameStateManager StateManager;

        private readonly IGameEngineListener _engineListener;

        public GameTime Time { get; private set; }

        protected Game(IGameEngineListener engineListener)
        {
            _engineListener = engineListener ?? throw new ArgumentNullException(nameof(engineListener));

            _engineListener.OnFrameEnter += OnFrameEnter;
            _engineListener.OnFrameExit += OnFrameExit;
            _engineListener.OnDispose += OnDispose;

            GameStateTreeStatic stateTree = CreateStateTree();

            StateManager = new GameStateManager(stateTree);
        }

        protected abstract GameStateTreeStatic CreateStateTree();
        protected abstract GameTime CaptureTime();

        private void OnFrameEnter()
        {
            Time = CaptureTime();
            
            StateManager.BeginFrame(this);
            StateManager.Update(this);
        }

        private void OnFrameExit()
        {
            StateManager.EndFrame(this);
        }

        private void OnDispose()
        {
            _engineListener.OnFrameEnter -= OnFrameEnter;
            _engineListener.OnFrameExit -= OnFrameExit;
            _engineListener.OnDispose -= OnDispose;
        }

    }

}