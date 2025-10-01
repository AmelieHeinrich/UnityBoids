using UnityEngine;

public class ISphereCollider : ICollider
{
    [SerializeField] public float Radius;

    public override float DistanceWithSphere(Vector3 position, float radius)
    {
        return Vector3.Distance(this.transform.position + new Vector3(Radius, Radius, Radius), position + new Vector3(radius, radius, radius));
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.transform.position, Radius);
    }
}
