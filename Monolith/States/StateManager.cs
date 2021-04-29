using System;
using System.Collections.Generic;

namespace Monolith.States
{

    public sealed class StateManager
    {

        public readonly StateTreeStatic Tree;

        private readonly Stack<State> _stack;

        private Type _target;
        private StateState _state;

        public StateManager(StateTreeStatic tree)
        {
            Tree = tree;

            _stack = new Stack<State>(1);
            _target = Tree.GetDefault().Type;
            _state = StateState.Uninitialized;
        }

        public T Get<T>() where T : State
        {
            T result = default;

            foreach (State state in _stack)
            {
                if (state is T castedState)
                {
                    result = castedState;
                    break;
                }
            }

            return result;
        }

        public void SetTarget<T>() where T : State
        {
            Type target = typeof(T);

            if (!Tree.Contains(target)) throw new ArgumentException("State could not be found.");

            _target = target;
        }

        internal void OnStartOfFrame(Game game)
        {
            bool isDone = false;

            do
            {
                State currentState = _stack.Count == 0 ? null : _stack.Peek();

                Type current = currentState?.GetType();
                Type parent = Tree.GetParent(current)?.Type;
                StateTreeNode nextNode = Tree.GetNext(current, _target);
                Type next = nextNode?.Type;

                switch (_state)
                {
                    case StateState.Uninitialized:
                        currentState = nextNode.Make(game);

                        _stack.Push(currentState);

                        _state = StateState.Loading;

                        break;
                    case StateState.Loading:
                        if (currentState.Load(game))
                        {
                            _state = StateState.Loaded;
                        }
                        else
                        {
                            isDone = true;
                        }

                        break;
                    case StateState.Loaded:
                        if (current == next)
                        {
                            _state = StateState.Activating;
                        }
                        else if (next != parent)
                        {
                            _state = StateState.Uninitialized;
                        }
                        else
                        {
                            isDone = true;
                        }

                        break;
                    case StateState.Activating:
                        currentState.Enter(game);
                        _state = StateState.Active;

                        isDone = true;

                        break;
                    default:
                        isDone = true;
                        break;
                }
            } while (!isDone);
        }

        internal void OnUpdate(Game game)
        {
            State currentState = _stack.Peek();

            Type current = currentState.GetType();
            Type next = Tree.GetNext(current, _target).Type;

            bool isTarget = current == next;

            if (isTarget && (_state == StateState.Active))
            {
                currentState.Update(game);
            }
        }

        internal void OnEndOfFrame(Game game)
        {
            bool isDone;

            do
            {
                State currentState = _stack.Count == 0 ? null : _stack.Peek();

                Type current = currentState?.GetType();
                Type parent = Tree.GetParent(current)?.Type;
                Type next = Tree.GetNext(current, _target)?.Type;

                switch (_state)
                {
                    default:
                        isDone = true;
                        break;
                    case StateState.Active:
                        isDone = current == next;

                        if (!isDone) _state = StateState.Deactivating;

                        break;
                    case StateState.Deactivating:
                        currentState.Exit(game);
                        isDone = false;
                        _state = StateState.Loaded;

                        break;
                    case StateState.Loaded:
                        isDone = next != parent;

                        if (!isDone) _state = StateState.Unloading;

                        break;
                    case StateState.Unloading:
                        isDone = !currentState.Unload(game);

                        if (!isDone)
                        {
                            _stack.Pop();

                            if (_stack.Count == 0)
                            {
                                _state = StateState.Uninitialized;
                            }
                            else
                            {
                                _state = StateState.Loaded;
                            }
                        }
                        break;
                }
            } while (!isDone);
        }

    }

}