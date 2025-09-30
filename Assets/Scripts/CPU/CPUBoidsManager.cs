using System.Collections.Generic;
using UnityEngine;

public class CPUBoidsManager : MonoBehaviour
{
    struct Boid
    {
        public Vector3 Position;
        public Vector3 Direction;
        public Vector3 Velocity;
        public Vector3 Acceleration;
        public GameObject Object;
    };

    [SerializeField] public int BoidCount = 3000;
    [SerializeField] public GameObject BoidPrefab;
    [SerializeField] public GameObjectPool BoidPool;
    private List<Boid> Boids;

    [SerializeField] public float NeighbourRadius = 6f;
    [SerializeField] public float SeparationRadius = 3f;
    [SerializeField] public float MaxSpeed = 2f;
    [SerializeField] public float MaxForce = 0.03f;
    [SerializeField] public float SeparationWeight = 0.15f;
    [SerializeField] public float AlignmentWeight = 0.02f;
    [SerializeField] public float CohesionWeight = 0.003f;
    [SerializeField] public int   MaxSeparationNeighbours = 20;
    [SerializeField] public int   MaxAlignmentNeighbours = 10;
    [SerializeField] public int   MaxCohesionNeighbours = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BoidPool = new GameObjectPool(BoidPrefab, BoidCount);
        Boids = new List<Boid>();

        // Spawn all boids
        for (int i = 0; i < BoidCount; i++)
        {
            Boid boid = new Boid();
            boid.Position = this.gameObject.transform.position + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            boid.Velocity = Random.insideUnitSphere.normalized;
            boid.Object = BoidPool.Get();

            Boids.Add(boid);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < BoidCount; i++)
        {
            Boid currBoid = Boids[i];

            // Update boid
            Vector3 separation = Vector3.zero;
            Vector3 alignment = Vector3.zero;
            Vector3 cohesion = Vector3.zero;

            int sepCount = 0;
            int aliCount = 0;
            int cohCount =0;

            // Neighbour search
            for (int j = 0; j < BoidCount; j++)
            {
                if (i == j) continue;
                {
                    Boid other = Boids[j];
                    float dist = Vector3.Distance(currBoid.Position, other.Position);

                    if (dist < NeighbourRadius)
                    {
                        // Separation
                        if (dist < SeparationRadius && sepCount < MaxSeparationNeighbours)
                        {
                            separation += (currBoid.Position - other.Position).normalized * (SeparationRadius - dist) / SeparationRadius;
                            sepCount++;
                        }

                        // Alignment
                        if (dist < NeighbourRadius && aliCount < MaxAlignmentNeighbours)
                        {
                            alignment += other.Velocity;
                            aliCount++;
                        }

                        // Cohesion
                        if (dist < NeighbourRadius && cohCount < MaxCohesionNeighbours)
                        {
                            cohesion += other.Position;
                            cohCount++;
                        }
                    }
                }
            }

            // Compute final forces
            if (aliCount > 0)
            {
                alignment /= aliCount;
                alignment -= currBoid.Velocity;
            }

            if (cohCount > 0)
            {
                cohesion /= cohCount;
                cohesion -= currBoid.Position;
            }

            Vector3 force = SeparationWeight * separation       
                          + AlignmentWeight  * alignment
                          + CohesionWeight   * cohesion;

            // Limit force 
            if (force.magnitude > MaxForce)
                force = force.normalized * MaxForce;

            // Update velocity 
            currBoid.Velocity += force;
            if (currBoid.Velocity.magnitude > MaxSpeed)
                currBoid.Velocity = currBoid.Velocity.normalized * MaxSpeed;

            // Update pos
            currBoid.Position += currBoid.Velocity * Time.deltaTime;

            // Synchronize GameObject
            currBoid.Object.transform.position = currBoid.Position;
            if (currBoid.Velocity != Vector3.zero)
                currBoid.Object.transform.rotation = Quaternion.LookRotation(currBoid.Velocity);

            // save the boid update 
            Boids[i] = currBoid;
        }
    }
}
