using UnityEngine;

public class IBoxCollider : ICollider
{
    [SerializeField] public Vector3 HalfExtents = Vector3.one;

    public override float DistanceWithSphere(Vector3 spherePos, float sphereRadius)
    {
        Vector3 obbCenter = this.transform.position;
        Quaternion invRot = Quaternion.Inverse(this.transform.rotation);
        Vector3 localSphere = invRot * (spherePos - obbCenter);

        Vector3 closestLocal = new Vector3(
            Mathf.Clamp(localSphere.x, -HalfExtents.x, HalfExtents.x),
            Mathf.Clamp(localSphere.y, -HalfExtents.y, HalfExtents.y),
            Mathf.Clamp(localSphere.z, -HalfExtents.z, HalfExtents.z)
        );

        Vector3 deltaLocal = localSphere - closestLocal;
        float distLocal = deltaLocal.magnitude;

        return distLocal - sphereRadius;
    }

    public override Vector3 ClosestPointOnSurface(Vector3 spherePos)
    {
        Vector3 obbCenter = this.transform.position;
        Quaternion invRot = Quaternion.Inverse(this.transform.rotation);
        Vector3 localSphere = invRot * (spherePos - obbCenter);

        Vector3 closestLocal = new Vector3(
            Mathf.Clamp(localSphere.x, -HalfExtents.x, HalfExtents.x),
            Mathf.Clamp(localSphere.y, -HalfExtents.y, HalfExtents.y),
            Mathf.Clamp(localSphere.z, -HalfExtents.z, HalfExtents.z)
        );

        return obbCenter + this.transform.rotation * closestLocal;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Matrix4x4 old = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(this.transform.position, this.transform.rotation, Vector3.one);
        Gizmos.DrawCube(Vector3.zero, 2f * HalfExtents);
        Gizmos.matrix = old;
    }
}
