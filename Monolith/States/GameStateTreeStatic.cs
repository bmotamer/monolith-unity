using System;
using System.Collections.Generic;

namespace Monolith.States
{

    public sealed class GameStateTreeStatic : GameStateTree
    {
        
        private static readonly int[] EmptyPath = new int[0];
        
        public override GameStateTreeNode Default { get; }

        internal GameStateTreeStatic(GameStateTreeNodeStatic[] roots, Dictionary<Type, int[]> paths, Type @default) : base(roots, paths)
        {
            Default = Find(@default);
        }
        
        internal GameStateTreeNode FindNext(Type current, Type target)
        {
            GameStateTreeNode next;

            if (current == target)
            {
                int[] currentPath = current == null ? EmptyPath : _paths[current];

                next = Find(currentPath, currentPath.Length);
            }
            else
            {
                int[] currentPath = current == null ? EmptyPath : _paths[current];
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

                next = backtrack ? Find(currentPath, currentPath.Length - 1) : Find(targetPath, currentPath.Length + 1);
            }

            return next;
        }

    }

}