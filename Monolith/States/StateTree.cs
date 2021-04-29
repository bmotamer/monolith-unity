using System;
using System.Collections.Generic;

namespace Monolith.States
{

    public abstract class StateTree
    {

        protected readonly IList<StateTreeNode> _roots;
        protected readonly Dictionary<Type, int[]> _paths;

        protected StateTree(IList<StateTreeNode> roots, Dictionary<Type, int[]> paths)
        {
            _roots = roots;
            _paths = paths;
        }

        public bool Contains<T>() where T : State => Contains(typeof(T));

        internal bool Contains(Type type) => _paths.ContainsKey(type);

        public StateTreeNode Get<T>() where T : State => Get(typeof(T));

        internal StateTreeNode Get(Type type)
        {
            StateTreeNode node = null;

            if (_paths.TryGetValue(type, out int[] path)) node = Get(path, path.Length);

            return node;
        }

        public abstract StateTreeNode GetDefault();

        public StateTreeNode GetParent<T>() where T : State => GetParent(typeof(T));

        internal StateTreeNode GetParent(Type type)
        {
            StateTreeNode node = null;

            if ((type != null) && _paths.TryGetValue(type, out int[] path)) node = Get(path, path.Length - 1);

            return node;
        }

        public StateTreeNode GetRoot<T>() where T : State => GetRoot(typeof(T));

        internal StateTreeNode GetRoot(Type type)
        {
            StateTreeNode node = null;

            if (_paths.TryGetValue(type, out int[] path)) node = Get(path, 1);

            return node;
        }

        protected StateTreeNode Get(int[] path, int pathLength)
        {
            if (pathLength <= 0) return null;

            StateTreeNode node = _roots[path[0]];

            for (int i = 1; i < pathLength; ++i) node = node.Children[path[i]];

            return node;
        }

    }

}