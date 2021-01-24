using System;

namespace Monolith.States
{

    public sealed class StateTreeNodeStatic : StateTreeNode
    {

        internal StateTreeNodeStatic(Type type, StateTreeNode[] children) : base(type, children)
        {
        }

    }

}