using System;

namespace Monolith
{

    public interface IGameEngineListener
    {

        event Action OnFrameEnter;
        event Action OnFrameExit;
        event Action OnDispose;

    }

}