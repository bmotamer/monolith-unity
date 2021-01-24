using System;

namespace Monolith.States
{

    public interface IState
    {

        bool Load(Game game);

        bool Enter(Game game);

        void Update(Game game);

        bool Exit(Game game);

        bool Unload(Game game);

    }

}