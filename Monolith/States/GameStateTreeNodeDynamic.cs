using System;
using System.Collections.Generic;

namespace Monolith.States
{

    public sealed class GameStateTreeNodeDynamic : GameStateTreeNode
    {

        public GameStateTreeNodeDynamic(Type type, Func<Game, GameState> make) : base(type, make, new List<GameStateTreeNode>())
        {
        }

        internal void AddChild(GameStateTreeNodeDynamic child) => _children.Add(child);

        public GameStateTreeNodeStatic AsStatic()
        {
            var children = new GameStateTreeNode[_children.Count];

            for (int i = 0; i < _children.Count; ++i) children[i] = ((GameStateTreeNodeDynamic)_children[i]).AsStatic();

            return new GameStateTreeNodeStatic(Type, Make, children);
        }

    }

}