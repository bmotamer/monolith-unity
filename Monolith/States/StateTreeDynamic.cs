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

        public void Add<TRoot>(Func<Game, TRoot> make) where TRoot : State => Add(null, typeof(TRoot), make);

        public void Add<TParent, TChild>(Func<Game, TChild> make) where TParent : State where TChild : State => Add(typeof(TParent), typeof(TChild), make);

        private void Add(Type parentType, Type childType, Func<Game, State> childMake)
        {
            StateTreeNodeDynamic child;

            if (parentType == null)
            {
                _paths.Add(childType, new[] { _roots.Count });

                child = new StateTreeNodeDynamic(childType, childMake);

                _roots.Add(child);
            }
            else
            {
                int[] parentPath = _paths[parentType];
                var parent = (StateTreeNodeDynamic)Get(parentPath, parentPath.Length);

                int[] childPath = new int[parentPath.Length + 1];

                for (int i = 0; i < parentPath.Length; ++i) childPath[i] = parentPath[i];

                childPath[parentPath.Length] = parent.Children.Count;

                _paths.Add(childType, childPath);

                child = new StateTreeNodeDynamic(childType, childMake);
                
                parent.AddChild(child);
            }

            _default ??= child;
        }

        public override StateTreeNode GetDefault() => _default;

        public void SetDefault<T>() where T : State => SetDefault(typeof(T));

        private void SetDefault(Type type)
        {
            StateTreeNode node = Get(type);

            _default = node ?? throw new ArgumentException("The given state could not be found in this tree.");
        }

        public StateTreeStatic AsStatic()
        {
            var roots = new StateTreeNodeStatic[_roots.Count];

            for (int i = 0; i < roots.Length; ++i) roots[i] = ((StateTreeNodeDynamic)_roots[i]).AsStatic();

            var paths = new Dictionary<Type, int[]>(_paths);

            return new StateTreeStatic(roots, paths, _default?.Type);
        }

    }

}