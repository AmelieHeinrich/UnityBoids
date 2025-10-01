using UnityEngine;

public class ISphereCollider : ICollider
{
    [SerializeField] public float Radius = 1f;

    // Distance réelle entre la surface de la sphère et le boid
    public override float DistanceWithSphere(Vector3 spherePos, float sphereRadius)
    {
        float centerDistance = Vector3.Distance(this.transform.position, spherePos);
        return centerDistance - (Radius + sphereRadius);
    }

    // Point le plus proche sur la surface de la sphère
    public override Vector3 ClosestPointOnSurface(Vector3 spherePos)
    {
        Vector3 dir = spherePos - this.transform.position;
        if (dir.sqrMagnitude > 1e-6f)
            dir = dir.normalized;
        else
            dir = Vector3.up; // fallback si exactement au centre

        return this.transform.position + dir * Radius;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.transform.position, Radius);
    }
}