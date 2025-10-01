using UnityEngine;

public abstract class ICollider : MonoBehaviour
{
    public abstract float DistanceWithSphere(Vector3 position, float radius);

    public abstract Vector3 ClosestPointOnSurface(Vector3 spherePos);
}
