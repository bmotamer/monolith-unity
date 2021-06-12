using System;

namespace Monolith.States
{

    public sealed class GameStateTreeNodeStatic : GameStateTreeNode
    {

        internal GameStateTreeNodeStatic(Type type, Func<Game, GameState> make, GameStateTreeNode[] children) : base(type, make, children)
        {
        }

    }

}