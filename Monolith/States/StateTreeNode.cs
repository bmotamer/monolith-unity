using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Monolith.States
{
    
    public abstract class StateTreeNode
    {

        public readonly Type Type;
        public readonly ReadOnlyCollection<StateTreeNode> Children;
        internal readonly Func<Game, State> Make;

        protected readonly IList<StateTreeNode> _children;

        protected StateTreeNode(Type type, Func<Game, State> make, IList<StateTreeNode> children)
        {
            Type = type;
            Make = make;

            _children = children;

            Children = new ReadOnlyCollection<StateTreeNode>(_children);
        }

    }

}