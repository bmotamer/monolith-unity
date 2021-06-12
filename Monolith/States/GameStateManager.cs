using System;
using System.Collections.Generic;

namespace Monolith.States
{

    public sealed class GameStateManager
    {

        public readonly GameStateTreeStatic Tree;

        private readonly Stack<GameState> _stack;
        private Type _target;
        private GameStateStep _step;

        public GameStateManager(GameStateTreeStatic tree)
        {
            Tree = tree;

            _stack = new Stack<GameState>(1);
            _target = Tree.Default.Type;
            _step = GameStateStep.Uninitialized;
        }

        public T Find<T>() where T : GameState
        {
            T result = null;

            foreach (GameState state in _stack)
            {
                if (state is T castedState)
                {
                    result = castedState;
                    break;
                }
            }

            return result;
        }

        public void SetTarget<T>() where T : GameState
        {
            Type target = typeof(T);

            if (!Tree.Contains(target)) throw new ArgumentException("State could not be found.");

            _target = target;
        }

        internal void BeginFrame(Game game)
        {
            bool isDone = false;

            do
            {
                GameState currentState = _stack.Count == 0 ? null : _stack.Peek();

                Type current = currentState?.GetType();
                Type parent = Tree.FindParent(current)?.Type;
                GameStateTreeNode nextNode = Tree.FindNext(current, _target);
                Type next = nextNode?.Type;

                switch (_step)
                {
                    case GameStateStep.Uninitialized:
                        currentState = nextNode.Make(game);

                        _stack.Push(currentState);

                        _step = GameStateStep.Loading;

                        break;
                    case GameStateStep.Loading:
                        if (currentState.Load(game))
                        {
                            _step = GameStateStep.Loaded;
                        }
                        else
                        {
                            isDone = true;
                        }

                        break;
                    case GameStateStep.Loaded:
                        if (current == next)
                        {
                            _step = GameStateStep.Activating;
                        }
                        else if (next != parent)
                        {
                            _step = GameStateStep.Uninitialized;
                        }
                        else
                        {
                            isDone = true;
                        }

                        break;
                    case GameStateStep.Activating:
                        currentState.Enter(game);
                        _step = GameStateStep.Active;

                        isDone = true;

                        break;
                    default:
                        isDone = true;
                        break;
                }
            } while (!isDone);
        }

        internal void Update(Game game)
        {
            GameState currentState = _stack.Peek();

            Type current = currentState.GetType();
            Type next = Tree.FindNext(current, _target).Type;

            bool isTarget = current == next;

            if (isTarget && (_step == GameStateStep.Active))
            {
                currentState.Update(game);
            }
        }

        internal void EndFrame(Game game)
        {
            bool isDone;

            do
            {
                GameState currentState = _stack.Count == 0 ? null : _stack.Peek();

                Type current = currentState?.GetType();
                Type parent = Tree.FindParent(current)?.Type;
                Type next = Tree.FindNext(current, _target)?.Type;

                switch (_step)
                {
                    default:
                        isDone = true;
                        break;
                    case GameStateStep.Active:
                        isDone = current == next;

                        if (!isDone) _step = GameStateStep.Deactivating;

                        break;
                    case GameStateStep.Deactivating:
                        currentState.Exit(game);
                        isDone = false;
                        _step = GameStateStep.Loaded;

                        break;
                    case GameStateStep.Loaded:
                        isDone = next != parent;

                        if (!isDone) _step = GameStateStep.Unloading;

                        break;
                    case GameStateStep.Unloading:
                        isDone = !currentState.Unload(game);

                        if (!isDone)
                        {
                            _stack.Pop();

                            if (_stack.Count == 0)
                            {
                                _step = GameStateStep.Uninitialized;
                            }
                            else
                            {
                                _step = GameStateStep.Loaded;
                            }
                        }
                        break;
                }
            } while (!isDone);
        }

    }

}