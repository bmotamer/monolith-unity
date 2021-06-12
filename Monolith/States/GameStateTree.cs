using System;
using System.Collections.Generic;

namespace Monolith.States
{

    public abstract class GameStateTree
    {

        protected readonly IList<GameStateTreeNode> _roots;
        protected readonly Dictionary<Type, int[]> _paths;

        protected GameStateTree(IList<GameStateTreeNode> roots, Dictionary<Type, int[]> paths)
        {
            _roots = roots;
            _paths = paths;
        }

        public bool Contains<T>() where T : GameState => Contains(typeof(T));

        internal bool Contains(Type type) => _paths.ContainsKey(type);

        public GameStateTreeNode Find<T>() where T : GameState => Find(typeof(T));

        internal GameStateTreeNode Find(Type type)
        {
            GameStateTreeNode node = null;

            if (_paths.TryGetValue(type, out int[] path)) node = Find(path, path.Length);

            return node;
        }

        public abstract GameStateTreeNode Default { get; }

        public GameStateTreeNode FindParent<T>() where T : GameState => FindParent(typeof(T));

        internal GameStateTreeNode FindParent(Type type)
        {
            GameStateTreeNode node = null;

            if ((type != null) && _paths.TryGetValue(type, out int[] path)) node = Find(path, path.Length - 1);

            return node;
        }

        public GameStateTreeNode FindRoot<T>() where T : GameState => FindRoot(typeof(T));

        internal GameStateTreeNode FindRoot(Type type)
        {
            GameStateTreeNode node = null;

            if (_paths.TryGetValue(type, out int[] path)) node = Find(path, 1);

            return node;
        }

        protected GameStateTreeNode Find(int[] path, int pathLength)
        {
            if (pathLength <= 0) return null;

            GameStateTreeNode node = _roots[path[0]];

            for (int i = 1; i < pathLength; ++i) node = node.Children[path[i]];

            return node;
        }

    }

}