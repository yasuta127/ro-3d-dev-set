using UnityEngine;

namespace Ro3dDevSet
{
    public static class PhysicsUtil
    {
        /// <summary>
        /// sphereColliderがtargetColliderに接しているか調べ、接している場合はclosestPosとsurfaceNormalを計算する
        /// </summary>
        /// <param name="targetCollider">sphereColliderが接しているかを調べる任意のコライダー</param>
        /// <param name="sphereCollider">targetColliderに接しているかを調べる球体コライダー</param>
        /// <param name="closestPos">
        /// targetCollider表面上の、sphereColliderの中心に最も近い点。
        /// 接触していない場合はゼロベクトルを返す
        /// </param>
        /// <param name="surfaceNormal">
        /// 接触しているtargetCollider表面の法線ベクトル。
        /// 接触していない場合はゼロベクトルを返す
        /// </param>
        /// <param name="surfacePenetrationDepth">
        /// sphereColliderをtargetColliderから引き離すのに必要なsurfaceNormalに沿った距離。
        /// 接触していない場合は0を返す
        /// </param>
        /// <param name="considerScale">sphereColliderのスケールを考慮するか</param>
        /// <returns>sphereColliderがtargetColliderに接しているか</returns>
        public static bool ComputeClosestPosition(Collider targetCollider, SphereCollider sphereCollider,
            out Vector3 closestPos, out Vector3 surfaceNormal, out float surfacePenetrationDepth,
            bool considerScale = false)
        {
            closestPos = Vector3.zero;
            surfaceNormal = Vector3.zero;
            surfacePenetrationDepth = 0f;

            if (targetCollider == sphereCollider) return false;

            Transform targetTrans = targetCollider.transform;
            Transform sphereTrans = sphereCollider.transform;
            Vector3 spherePos = sphereTrans.position;

            var scale = considerScale ? sphereTrans.localScale.x : 1f;

            bool isOverlap = Physics.ComputePenetration(targetCollider, targetTrans.position, targetTrans.rotation,
                sphereCollider, spherePos, Quaternion.identity, out surfaceNormal, out surfacePenetrationDepth);

            if (isOverlap)
            {
                closestPos = spherePos + surfaceNormal * (sphereCollider.radius * scale - surfacePenetrationDepth);
                surfaceNormal = -surfaceNormal;
            }

            return isOverlap;
        }

        public static bool ComputeClosestPosition(Collider targetCollider, SphereCollider sphereCollider,
            out Vector3 closestPos, out Vector3 surfaceNormal)
        {
            return ComputeClosestPosition(targetCollider, sphereCollider, out closestPos, out surfaceNormal, out _);
        }
    }
}