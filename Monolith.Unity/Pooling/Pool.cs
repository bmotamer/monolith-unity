using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Monolith.Unity.Pooling
{
    
    public sealed class Pool
    {
        
        private readonly PoolObject _source;

        private PoolObject _inactiveTail;

        private PoolObject _activeHead;

        private PoolObject _activeTail;

        public Pool(PoolObject source)
        {
            if (!source) throw new ArgumentNullException();
            
            _source = source;
        }

        public PoolObject NewestActive => _activeTail;
        public PoolObject OldestActive => _activeHead;

        #region Instantiate

        public int Instantiate(int count)
        {
            if (count < 1) return 0;

            for (int i = 0; i < count; ++i)
            {
                if ((object)Instantiate() == null)
                {
                    Debug.LogError(_source.gameObject.name + " instantiation failed: out of memory. Only " +
                                   i.ToString()+ " clones out of " + count.ToString()+ " were successfully created.");

                    return i;
                }
            }

            return count;
        }

        public PoolObject Instantiate()
        {
            PoolObject clone;

            try
            {
                clone = Object.Instantiate(_source);
            }
            catch
            {
                clone = null;
            }

            if ((object)clone != null)
            {
                clone.GameObject = clone.gameObject;
                clone.GameObject.hideFlags = HideFlags.HideInHierarchy;

                Object.DontDestroyOnLoad(clone.GameObject);

                clone.Pool = this;

                if ((object)_inactiveTail != null)
                {
                    clone.Previous = _inactiveTail;

                    _inactiveTail.Next = clone;
                }

                _inactiveTail = clone;

                clone.OnInstantiate();
                clone.GameObject.SetActive(false);
            }

            return clone;
        }

        #endregion

        #region Peek

        public PoolObject Peek()
        {
            if ((object)_inactiveTail == null) return _activeHead;

            return _inactiveTail;
        }

        public PoolObject PeekDebug()
        {
            PoolObject clone;

            if ((object)_inactiveTail == null)
            {
                try
                {
                    clone = Object.Instantiate(_source);
                }
                catch
                {
                    clone = null;
                }

                if ((object)clone == null)
                {
                    Debug.LogError(_source.gameObject.name +
                                   " instantiation failed: out of memory. Peeking existing clones instead.");

                    clone = _activeHead;
                }
                else
                {
                    clone.GameObject = clone.gameObject;
                    clone.GameObject.hideFlags = HideFlags.HideInHierarchy;

                    Object.DontDestroyOnLoad(clone.GameObject);

                    clone.Pool = this;

                    _inactiveTail = clone;

                    clone.OnInstantiate();

                    clone.GameObject.SetActive(false);
                }
            }
            else
            {
                clone = _inactiveTail;
            }

            return clone;
        }

        #endregion

        #region Spawn

        public PoolObject Spawn()
        {
            PoolObject clone;

            if ((object)_inactiveTail == null)
            {
                clone = _activeHead;

                if ((object)_activeHead != _activeTail)
                {
                    _activeHead = _activeHead.Next;
                    _activeHead.Previous = null;

                    clone.Previous = _activeTail;
                    clone.Next = null;

                    _activeTail.Next = clone;
                    _activeTail = clone;
                }

                clone.OnRespawn();
            }
            else
            {
                clone = _inactiveTail;

                _inactiveTail = _inactiveTail.Previous;

                if ((object)_inactiveTail != null)
                    _inactiveTail.Next = null;

                clone.Previous = _activeTail;

                if ((object)_activeHead == null)
                {
                    _activeHead = clone;
                    _activeTail = _activeHead;
                }
                else
                {
                    _activeTail.Next = clone;
                    _activeTail = clone;
                }

                clone.OnSpawn();

                clone.IsSpawned = true;

                clone.GameObject.SetActive(true);
            }

            return clone;
        }

        public PoolObject SpawnDebug()
        {
            PoolObject clone;

            if ((object)_inactiveTail == null)
            {
                try
                {
                    clone = Object.Instantiate(_source);
                }
                catch
                {
                    clone = null;
                }

                if ((object)clone == null)
                {
                    Debug.LogError(_source.gameObject.name +
                                   " instantiation failed: out of memory. Respawning clones instead.");

                    clone = _activeHead;

                    if ((object)_activeHead != _activeTail)
                    {
                        _activeHead = _activeHead.Next;
                        _activeHead.Previous = null;

                        clone.Previous = _activeTail;
                        clone.Next = null;

                        _activeTail.Next = clone;
                        _activeTail = clone;
                    }

                    clone.OnRespawn();
                }
                else
                {
                    clone.GameObject = clone.gameObject;
                    clone.GameObject.hideFlags = HideFlags.HideInHierarchy;

                    Object.DontDestroyOnLoad(clone.GameObject);

                    clone.Pool = this;
                    clone.Previous = _activeTail;

                    _activeTail.Next = clone;
                    _activeTail = clone;

                    clone.OnInstantiate();
                    clone.OnSpawn();

                    clone.IsSpawned = true;

                    clone.GameObject.SetActive(true);
                }
            }
            else
            {
                clone = _inactiveTail;

                _inactiveTail = _inactiveTail.Previous;

                if ((object)_inactiveTail != null) _inactiveTail.Next = null;

                clone.Previous = _activeTail;

                if ((object)_activeHead == null)
                {
                    _activeHead = clone;
                    _activeTail = _activeHead;
                }
                else
                {
                    _activeTail.Next = clone;
                    _activeTail = clone;
                }

                clone.OnSpawn();

                clone.IsSpawned = true;

                clone.GameObject.SetActive(true);
            }

            return clone;
        }

        internal void Spawn(PoolObject clone)
        {
            if ((object)clone.Previous == null)
            {
                if ((object)clone.Next == null)
                {
                    _inactiveTail = null;
                }
                else
                {
                    clone.Next.Previous = null;
                    clone.Next = null;
                }
            }
            else if ((object)clone.Next == null)
            {
                _inactiveTail = clone.Previous;
                _inactiveTail.Next = null;
            }
            else
            {
                clone.Previous.Next = clone.Next;
                clone.Next.Previous = clone.Previous;

                clone.Next = null;
            }

            clone.Previous = _activeTail;

            if ((object)_activeHead == null)
            {
                _activeHead = clone;
                _activeTail = _activeHead;
            }
            else
            {
                _activeTail.Next = clone;
                _activeTail = clone;
            }

            clone.OnSpawn();

            clone.IsSpawned = true;

            clone.GameObject.SetActive(true);
        }

        internal void Respawn(PoolObject clone)
        {
            if ((object)clone.Previous == null)
            {
                if ((object)clone.Next != null)
                {
                    _activeHead = _activeHead.Next;
                    _activeHead.Previous = null;

                    clone.Previous = _activeTail;
                    clone.Next = null;

                    _activeTail.Next = clone;
                    _activeTail = clone;
                }
            }
            else if ((object)clone.Next != null)
            {
                clone.Previous.Next = clone.Next;
                clone.Next.Previous = clone.Previous;

                clone.Previous = _activeTail;
                clone.Next = null;

                _activeTail.Next = clone;
                _activeTail = clone;
            }

            clone.OnRespawn();
        }

        #endregion

        #region Despawn

        internal void Despawn(PoolObject clone)
        {
            if ((object)clone.Previous == null)
            {
                if ((object)_activeHead == _activeTail)
                {
                    _activeHead = null;
                    _activeTail = null;
                }
                else
                {
                    _activeHead = _activeHead.Next;
                    _activeHead.Previous = null;
                }
            }
            else if ((object)clone.Next == null)
            {
                _activeTail = _activeTail.Previous;
                _activeTail.Next = null;
            }
            else
            {
                clone.Previous.Next = clone.Next;
                clone.Next.Previous = clone.Previous;
            }

            clone.Previous = _inactiveTail;
            clone.Next = null;

            if ((object)_inactiveTail != null) _inactiveTail.Next = clone;

            _inactiveTail = clone;

            clone.IsSpawned = false;

            clone.OnDespawn();
            clone.GameObject.SetActive(false);
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            PoolObject currentClone = _inactiveTail;

            while ((object)currentClone != null)
            {
                currentClone.OnDispose();

                GameObject currentGameObject = currentClone.GameObject;
                PoolObject previousClone = currentClone.Previous;

                currentClone.GameObject = null;
                currentClone.Pool = null;
                currentClone.Previous = null;
                currentClone.Next = null;
                currentClone.IsSpawned = false;

                Object.Destroy(currentGameObject);

                currentClone = previousClone;
            }

            _inactiveTail = null;

            currentClone = _activeHead;

            while ((object)currentClone != null)
            {
                currentClone.OnDispose();

                GameObject currentGameObject = currentClone.GameObject;
                PoolObject nextClone = currentClone.Next;

                currentClone.GameObject = null;
                currentClone.Pool = null;
                currentClone.Previous = null;
                currentClone.Next = null;
                currentClone.IsSpawned = false;

                Object.Destroy(currentGameObject);

                currentClone = nextClone;
            }

            _activeHead = null;
            _activeTail = null;
        }

        #endregion
        
    }
    
}