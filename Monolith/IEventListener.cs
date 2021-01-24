using System;

namespace Monolith
{

    public interface IEventListener
    {

        event Action OnFrameEnter;
        event Action OnFrameExit;
        event Action OnDispose;

    }

}