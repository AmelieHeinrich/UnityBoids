using UnityEngine;

public abstract class ICollider : MonoBehaviour
{
    public abstract float DistanceWithSphere(Vector3 position, float radius);
}
