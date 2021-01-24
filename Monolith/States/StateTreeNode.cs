using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Monolith.States
{

    public abstract class StateTreeNode
    {

        public readonly Type Type;
        public readonly ReadOnlyCollection<StateTreeNode> Children;

        protected readonly IList<StateTreeNode> _children;

        protected StateTreeNode(Type type, IList<StateTreeNode> children)
        {
            Type = type;

            _children = children;

            Children = new ReadOnlyCollection<StateTreeNode>(_children);
        }

    }

}