using System;
using System.Collections.Generic;

namespace Monolith.States
{

    public sealed class StateTreeDynamic : StateTree
    {

        private StateTreeNode _default;

        public StateTreeDynamic() : base(new List<StateTreeNode>(), new Dictionary<Type, int[]>())
        {
        }

        public void Add<TRoot>() where TRoot : IState => Add(null, typeof(TRoot));

        public void Add<TParent, TChild>() where TParent : IState where TChild : IState => Add(typeof(TParent), typeof(TChild));

        private void Add(Type parentType, Type childType)
        {
            StateTreeNodeDynamic child;

            if (parentType == null)
            {
                _paths.Add(childType, new int[] { _roots.Count });

                child = new StateTreeNodeDynamic(childType);

                _roots.Add(child);
            }
            else
            {
                int[] parentPath = _paths[parentType];
                StateTreeNodeDynamic parent = (StateTreeNodeDynamic)Get(parentPath, parentPath.Length);

                int[] childPath = new int[parentPath.Length + 1];

                for (int i = 0; i < parentPath.Length; ++i) childPath[i] = parentPath[i];

                childPath[parentPath.Length] = parent.Children.Count;

                _paths.Add(childType, childPath);

                child = new StateTreeNodeDynamic(childType);
                
                parent.AddChild(child);
            }

            if (_default == null) _default = child;
        }

        public override StateTreeNode GetDefault() => _default;

        public void SetDefault<T>() where T : IState => SetDefault(typeof(T));

        private void SetDefault(Type type)
        {
            StateTreeNode node = Get(type);

            if (node == null) throw new ArgumentException("The given state could not be found in this tree.");

            _default = node;
        }

        public StateTreeStatic AsStatic()
        {
            StateTreeNodeStatic[] roots = new StateTreeNodeStatic[_roots.Count];

            for (int i = 0; i < roots.Length; ++i) roots[i] = ((StateTreeNodeDynamic)_roots[i]).AsStatic();

            Dictionary<Type, int[]> paths = new Dictionary<Type, int[]>(_paths);

            return new StateTreeStatic(roots, paths, _default?.Type);
        }

    }

}