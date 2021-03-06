﻿using System;
using System.Collections.Generic;

namespace Monolith.States
{

    public sealed class StateTreeNodeDynamic : StateTreeNode
    {

        public StateTreeNodeDynamic(Type type) : base(type, new List<StateTreeNode>())
        {
        }

        internal void AddChild(StateTreeNodeDynamic child) => _children.Add(child);

        public StateTreeNodeStatic AsStatic()
        {
            StateTreeNode[] children = new StateTreeNode[_children.Count];

            for (int i = 0; i < _children.Count; ++i) children[i] = ((StateTreeNodeDynamic)_children[i]).AsStatic();

            return new StateTreeNodeStatic(Type, children);
        }

    }

}