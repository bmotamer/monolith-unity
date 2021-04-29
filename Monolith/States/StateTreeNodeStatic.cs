using System;

namespace Monolith.States
{

    public sealed class StateTreeNodeStatic : StateTreeNode
    {

        internal StateTreeNodeStatic(Type type, Func<Game, State> make, StateTreeNode[] children) : base(type, make, children)
        {
        }

    }

}