using System;
using System.Collections.Generic;

namespace Monolith.States
{

    public sealed class GameStateTreeDynamic : GameStateTree
    {

        private GameStateTreeNode _default;

        public GameStateTreeDynamic() : base(new List<GameStateTreeNode>(), new Dictionary<Type, int[]>())
        {
        }

        public void Add<TRoot>(Func<Game, TRoot> make) where TRoot : GameState => Add(null, typeof(TRoot), make);

        public void Add<TParent, TChild>(Func<Game, TChild> make) where TParent : GameState where TChild : GameState => Add(typeof(TParent), typeof(TChild), make);

        private void Add(Type parentType, Type childType, Func<Game, GameState> childMake)
        {
            GameStateTreeNodeDynamic child;

            if (parentType == null)
            {
                _paths.Add(childType, new[] { _roots.Count });

                child = new GameStateTreeNodeDynamic(childType, childMake);

                _roots.Add(child);
            }
            else
            {
                int[] parentPath = _paths[parentType];
                var parent = (GameStateTreeNodeDynamic)Find(parentPath, parentPath.Length);

                int[] childPath = new int[parentPath.Length + 1];

                for (int i = 0; i < parentPath.Length; ++i) childPath[i] = parentPath[i];

                childPath[parentPath.Length] = parent.Children.Count;

                _paths.Add(childType, childPath);

                child = new GameStateTreeNodeDynamic(childType, childMake);
                
                parent.AddChild(child);
            }

            _default ??= child;
        }

        public override GameStateTreeNode Default => _default;

        public void SetDefault<T>() where T : GameState => SetDefault(typeof(T));

        private void SetDefault(Type type)
        {
            GameStateTreeNode node = Find(type);

            _default = node ?? throw new ArgumentException("The given state could not be found in this tree.");
        }

        public GameStateTreeStatic AsStatic()
        {
            var roots = new GameStateTreeNodeStatic[_roots.Count];

            for (int i = 0; i < roots.Length; ++i) roots[i] = ((GameStateTreeNodeDynamic)_roots[i]).AsStatic();

            var paths = new Dictionary<Type, int[]>(_paths);

            return new GameStateTreeStatic(roots, paths, _default?.Type);
        }

    }

}