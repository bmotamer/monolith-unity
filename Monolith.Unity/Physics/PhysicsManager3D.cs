using UnityEngine;
using UnityEngine.SceneManagement;

using Physics3D = UnityEngine.Physics;

namespace Monolith.Unity.Physics
{

    public sealed class PhysicsManager3D : PhysicsManager
    {

        private readonly PhysicsScene _world;

        public PhysicsManager3D(Scene scene) : base()
        {
            _world = scene.GetPhysicsScene();
        }

        #region CastLine

        public bool CastLine(Vector3 origin, Vector3 direction, float maxDistance = Mathf.Infinity, int layerMask = Physics3D.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
        {
            return _world.Raycast(origin, direction, maxDistance, layerMask, queryTriggerInteraction);
        }

        public bool CastLine(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance = Mathf.Infinity, int layerMask = Physics3D.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
        {
            return _world.Raycast(origin, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
        }

        public int CastLine(Vector3 origin, Vector3 direction, RaycastHit[] raycastHits, float maxDistance = Mathf.Infinity, int layerMask = Physics3D.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
        {
            return _world.Raycast(origin, direction, raycastHits, maxDistance, layerMask, queryTriggerInteraction);
        }

        #endregion

        #region CastBall

        public bool CastBall(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance = Mathf.Infinity, int layerMask = Physics3D.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
        {
            return _world.SphereCast(origin, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
        }

        public int CastBall(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, float maxDistance = Mathf.Infinity, int layerMask = Physics3D.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
        {
            return _world.SphereCast(origin, radius, direction, results, maxDistance, layerMask, queryTriggerInteraction);
        }

        #endregion

        #region CastCapsule

        public bool CastCapsule(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance = Mathf.Infinity, int layerMask = Physics3D.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
        {
            return _world.CapsuleCast(point1, point2, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
        }

        public int CastCapsule(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results, float maxDistance = Mathf.Infinity, int layerMask = Physics3D.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
        {
            return _world.CapsuleCast(point1, point2, radius, direction, results, maxDistance, layerMask, queryTriggerInteraction);
        }

        #endregion

        #region CastBox

        public bool CastBox(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, Quaternion orientation, float maxDistance = Mathf.Infinity, int layerMask = Physics3D.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
        {
            return _world.BoxCast(center, halfExtents, direction, out hitInfo, orientation, maxDistance, layerMask, queryTriggerInteraction);
        }

        public int CastBox(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, Quaternion orientation, float maxDistance = Mathf.Infinity, int layerMask = Physics3D.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
        {
            return _world.BoxCast(center, halfExtents, direction, results, orientation, maxDistance, layerMask, queryTriggerInteraction);
        }

        #endregion

        #region OverlapBall

        public int OverlapBall(Vector3 position, float radius, Collider[] results, int layerMask = Physics3D.AllLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
        {
            return _world.OverlapSphere(position, radius, results, layerMask, queryTriggerInteraction);
        }

        #endregion

        #region OverlapCapsule

        public int OverlapCapsule(Vector3 point0, Vector3 point1, float radius, Collider[] results, int layerMask = Physics3D.AllLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
        {
            return _world.OverlapCapsule(point0, point1, radius, results, layerMask, queryTriggerInteraction);
        }

        #endregion

        #region OverlapBox

        public int OverlapBox(Vector3 center, Vector3 halfExtents, Collider[] results, Quaternion orientation, int layerMask = Physics3D.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
        {
            return _world.OverlapBox(center, halfExtents, results, orientation, layerMask, queryTriggerInteraction);
        }

        #endregion

        protected override void Simulate(float fixedDeltaTime) => _world.Simulate(fixedDeltaTime);

    }

}