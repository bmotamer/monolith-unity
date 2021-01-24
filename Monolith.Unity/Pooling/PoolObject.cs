using UnityEngine;

namespace Monolith.Unity.Pooling
{

    public abstract class PoolObject : MonoBehaviour
    {

        internal Pool Pool;
        internal PoolObject Previous;
        internal PoolObject Next;

        public GameObject GameObject { get; internal set; }
        public bool IsSpawned { get; internal set; }

        protected internal abstract void OnInstantiate();
        protected internal abstract void OnSpawn();
        protected internal abstract void OnRespawn();
        protected internal abstract void OnDespawn();
        protected internal abstract void OnDispose();

        public void Spawn()
        {
            if (IsSpawned)
            {
                Pool.Respawn(this);
            }
            else
            {
                Pool.Spawn(this);
            }
        }

        public void Despawn()
        {
            if (IsSpawned) Pool.Despawn(this);
        }

    }

}