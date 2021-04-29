using System;
using System.Collections.Generic;

namespace Monolith.States
{

    public sealed class StateTreeStatic : StateTree
    {

        private readonly StateTreeNode _default;
        private readonly int[] _emptyPath = new int[0];

        internal StateTreeStatic(StateTreeNodeStatic[] roots, Dictionary<Type, int[]> paths, Type @default) : base(roots, paths)
        {
            _default = Get(@default);
        }

        public override StateTreeNode GetDefault() => _default;

        internal StateTreeNode GetNext(Type current, Type target)
        {
            StateTreeNode next;

            if (current == target)
            {
                int[] currentPath = current == null ? _emptyPath : _paths[current];

                next = Get(currentPath, currentPath.Length);
            }
            else
            {
                int[] currentPath = current == null ? _emptyPath : _paths[current];
                int[] targetPath = _paths[target];

                bool backtrack = currentPath.Length > targetPath.Length;

                if (!backtrack)
                {
                    for (int i = 0; i < currentPath.Length; ++i)
                    {
                        if (currentPath[i] != targetPath[i])
                        {
                            backtrack = true;
                            break;
                        }
                    }
                }

                next = backtrack ? Get(currentPath, currentPath.Length - 1) : Get(targetPath, currentPath.Length + 1);
            }

            return next;
        }

    }

}