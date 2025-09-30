using UnityEngine;
using UnityEngine.Rendering;

public class LimitArea : MonoBehaviour

{
    public float radius = 20f;
    [SerializeField] private float buffer = 5.5f;   // buffer zone before the edge 
    [SerializeField] private float strength = 25f; // repulsion force
    private CPUBoidsManager boid;

    private void OnDrawGizmos()
    {
        //main zone 
        Gizmos.color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
        //Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawSphere(transform.position, radius);

        //just for the visual to check 
        Gizmos.color = new Color(1.0f, 0.0f, 1.0f, 0.3f);
        //Gizmos.DrawWireSphere(transform.position, radius - buffer);
        Gizmos.DrawSphere(transform.position, radius - buffer);
    }

    // 
    public Vector3 GetRepulsionForce(Vector3 Position)
    {
        Vector3 center = transform.position;    
        Vector3 dirFromCenter = Position - center;
        float distance = dirFromCenter.magnitude;

        if (distance > radius - buffer)
        {
            float t = (distance - (radius -buffer)) / buffer;
            t= Mathf.Clamp01(t); // make sure t stay between 0 and 1

            //back on center 
            Vector3 pushDir = -dirFromCenter.normalized; 

            return pushDir * (t * strength);
        }
        // if inside the safe zone >> no force
        return Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
