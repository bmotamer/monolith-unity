using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Monolith.States
{
    
    public abstract class GameStateTreeNode
    {

        public readonly Type Type;
        public readonly ReadOnlyCollection<GameStateTreeNode> Children;
        internal readonly Func<Game, GameState> Make;

        protected readonly IList<GameStateTreeNode> _children;

        protected GameStateTreeNode(Type type, Func<Game, GameState> make, IList<GameStateTreeNode> children)
        {
            Type = type;
            Make = make;

            _children = children;

            Children = new ReadOnlyCollection<GameStateTreeNode>(_children);
        }

    }

}