using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Monolith.Unity.Physics
{

    public sealed class PhysicsManager2D : PhysicsManager
    {

        private PhysicsScene2D _world;

        public PhysicsManager2D(Scene scene) : base()
        {
            _world = scene.GetPhysicsScene2D();
        }

        #region CastLine

        public RaycastHit2D CastLine(Vector2 origin, Vector2 direction, float distance, int layerMask = Physics2D.DefaultRaycastLayers)
        {
            return _world.Raycast(origin, direction, distance, layerMask);
        }

        public RaycastHit2D CastLine(Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter)
        {
            return _world.Raycast(origin, direction, distance, contactFilter);
        }

        public int CastLine(Vector2 origin, Vector2 direction, float distance, RaycastHit2D[] results, int layerMask = Physics2D.DefaultRaycastLayers)
        {
            return _world.Raycast(origin, direction, distance, results, layerMask);
        }

        public int CastLine(Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter, RaycastHit2D[] results)
        {
            return _world.Raycast(origin, direction, distance, contactFilter, results);
        }

        public int CastLine(Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter, List<RaycastHit2D> results)
        {
            return _world.Raycast(origin, direction, distance, contactFilter, results);
        }

        public RaycastHit2D CastLine(Vector2 start, Vector2 end, int layerMask = Physics2D.DefaultRaycastLayers)
        {
            return _world.Linecast(start, end, layerMask);
        }

        public RaycastHit2D CastLine(Vector2 start, Vector2 end, ContactFilter2D contactFilter)
        {
            return _world.Linecast(start, end, contactFilter);
        }

        public int CastLine(Vector2 start, Vector2 end, RaycastHit2D[] results, int layerMask = Physics2D.DefaultRaycastLayers)
        {
            return _world.Linecast(start, end, results, layerMask);
        }

        public int CastLine(Vector2 start, Vector2 end, ContactFilter2D contactFilter, RaycastHit2D[] results)
        {
            return _world.Linecast(start, end, contactFilter, results);
        }

        public int CastLine(Vector2 start, Vector2 end, ContactFilter2D contactFilter, List<RaycastHit2D> results)
        {
            return _world.Linecast(start, end, contactFilter, results);
        }

        #endregion

        #region CastCircle

        public RaycastHit2D CastCircle(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask = Physics2D.DefaultRaycastLayers)
        {
            return _world.CircleCast(origin, radius, direction, distance, layerMask);
        }

        public RaycastHit2D CastCircle(Vector2 origin, float radius, Vector2 direction, float distance, ContactFilter2D contactFilter)
        {
            return _world.CircleCast(origin, radius, direction, distance, contactFilter);
        }

        public int CastCircle(Vector2 origin, float radius, Vector2 direction, float distance, RaycastHit2D[] results, int layerMask = Physics2D.DefaultRaycastLayers)
        {
            return _world.CircleCast(origin, radius, direction, distance, results, layerMask);
        }

        public int CastCircle(Vector2 origin, float radius, Vector2 direction, float distance, ContactFilter2D contactFilter, RaycastHit2D[] results)
        {
            return _world.CircleCast(origin, radius, direction, distance, contactFilter, results);
        }

        public int CastCircle(Vector2 origin, float radius, Vector2 direction, float distance, ContactFilter2D contactFilter, List<RaycastHit2D> results)
        {
            return _world.CircleCast(origin, radius, direction, distance, contactFilter, results);
        }

        #endregion

        #region CastCapsule

        public RaycastHit2D CastCapsule(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, int layerMask = Physics2D.DefaultRaycastLayers)
        {
            return _world.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, layerMask);
        }

        public RaycastHit2D CastCapsule(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter)
        {
            return _world.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, contactFilter);
        }

        public int CastCapsule(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, RaycastHit2D[] results, int layerMask = Physics2D.DefaultRaycastLayers)
        {
            return _world.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, results, layerMask);
        }

        public int CastCapsule(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, RaycastHit2D[] results)
        {
            return _world.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, contactFilter, results);
        }

        public int CastCapsule(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, List<RaycastHit2D> results)
        {
            return _world.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, contactFilter, results);
        }

        #endregion

        #region CastBox

        public RaycastHit2D CastBox(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask = Physics2D.DefaultRaycastLayers)
        {
            return _world.BoxCast(origin, size, angle, direction, distance, layerMask);
        }

        public RaycastHit2D CastBox(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter)
        {
            return _world.BoxCast(origin, size, angle, direction, distance, contactFilter);
        }

        public int CastBox(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, RaycastHit2D[] results, int layerMask = Physics2D.DefaultRaycastLayers)
        {
            return _world.BoxCast(origin, size, angle, direction, distance, results, layerMask);
        }

        public int CastBox(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, RaycastHit2D[] results)
        {
            return _world.BoxCast(origin, size, angle, direction, distance, contactFilter, results);
        }

        public int CastBox(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, List<RaycastHit2D> results)
        {
            return _world.BoxCast(origin, size, angle, direction, distance, contactFilter, results);
        }

        #endregion

        #region OverlapPoint

        public Collider2D OverlapPoint(Vector2 point, int layerMask = Physics2D.DefaultRaycastLayers)
        {
            return _world.OverlapPoint(point, layerMask);
        }

        public Collider2D OverlapPoint(Vector2 point, ContactFilter2D contactFilter)
        {
            return _world.OverlapPoint(point, contactFilter);
        }

        public int OverlapPoint(Vector2 point, Collider2D[] results, int layerMask = Physics2D.DefaultRaycastLayers)
        {
            return _world.OverlapPoint(point, results, layerMask);
        }

        public int OverlapPoint(Vector2 point, ContactFilter2D contactFilter, Collider2D[] results)
        {
            return _world.OverlapPoint(point, contactFilter, results);
        }

        public int OverlapPoint(Vector2 point, ContactFilter2D contactFilter, List<Collider2D> results)
        {
            return _world.OverlapPoint(point, contactFilter, results);
        }

        #endregion

        #region OverlapCircle

        public Collider2D OverlapCircle(Vector2 point, float radius, int layerMask = Physics2D.DefaultRaycastLayers)
        {
            return _world.OverlapCircle(point, radius, layerMask);
        }

        public Collider2D OverlapCircle(Vector2 point, float radius, ContactFilter2D contactFilter)
        {
            return _world.OverlapCircle(point, radius, contactFilter);
        }

        public int OverlapCircle(Vector2 point, float radius, Collider2D[] results, int layerMask = Physics2D.DefaultRaycastLayers)
        {
            return _world.OverlapCircle(point, radius, results, layerMask);
        }

        public int OverlapCircle(Vector2 point, float radius, ContactFilter2D contactFilter, Collider2D[] results)
        {
            return _world.OverlapCircle(point, radius, contactFilter, results);
        }

        public int OverlapCircle(Vector2 point, float radius, ContactFilter2D contactFilter, List<Collider2D> results)
        {
            return _world.OverlapCircle(point, radius, contactFilter, results);
        }

        #endregion

        #region OverlapCapsule

        public Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, int layerMask = Physics2D.DefaultRaycastLayers)
        {
            return _world.OverlapCapsule(point, size, direction, angle, layerMask);
        }

        public Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter)
        {
            return _world.OverlapCapsule(point, size, direction, angle, contactFilter);
        }

        public int OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, Collider2D[] results, int layerMask = Physics2D.DefaultRaycastLayers)
        {
            return _world.OverlapCapsule(point, size, direction, angle, results, layerMask);
        }

        public int OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter, Collider2D[] results)
        {
            return _world.OverlapCapsule(point, size, direction, angle, contactFilter, results);
        }

        public int OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter, List<Collider2D> results)
        {
            return _world.OverlapCapsule(point, size, direction, angle, contactFilter, results);
        }

        #endregion

        #region OverlapBox

        public Collider2D OverlapBox(Vector2 point, Vector2 size, float angle, int layerMask = Physics2D.DefaultRaycastLayers)
        {
            return _world.OverlapBox(point, size, angle, layerMask);
        }

        public Collider2D OverlapBox(Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter)
        {
            return _world.OverlapBox(point, size, angle, contactFilter);
        }

        public int OverlapBox(Vector2 point, Vector2 size, float angle, Collider2D[] results, int layerMask = Physics2D.DefaultRaycastLayers)
        {
            return _world.OverlapBox(point, size, angle, results, layerMask);
        }

        public int OverlapBox(Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter, Collider2D[] results)
        {
            return _world.OverlapBox(point, size, angle, contactFilter, results);
        }

        public int OverlapBox(Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter, List<Collider2D> results)
        {
            return _world.OverlapBox(point, size, angle, contactFilter, results);
        }

        public Collider2D OverlapBox(Vector2 pointA, Vector2 pointB, int layerMask = Physics2D.DefaultRaycastLayers)
        {
            return _world.OverlapArea(pointA, pointB, layerMask);
        }

        public Collider2D OverlapBox(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter)
        {
            return _world.OverlapArea(pointA, pointB, contactFilter);
        }

        public int OverlapBox(Vector2 pointA, Vector2 pointB, Collider2D[] results, int layerMask = Physics2D.DefaultRaycastLayers)
        {
            return _world.OverlapArea(pointA, pointB, results, layerMask);
        }

        public int OverlapBox(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter, Collider2D[] results)
        {
            return _world.OverlapArea(pointA, pointB, contactFilter, results);
        }

        public int OverlapBox(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter, List<Collider2D> results)
        {
            return _world.OverlapArea(pointA, pointB, contactFilter, results);
        }

        #endregion

        protected override void SimulateInternal(float fixedDeltaTime) => _world.Simulate(fixedDeltaTime);

    }

}