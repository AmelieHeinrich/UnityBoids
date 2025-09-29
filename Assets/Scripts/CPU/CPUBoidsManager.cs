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
            float neigborRadius = 6f;
            float separationRadius = 3f;
            float maxSpeed = 2f;
            float maxForce = 0.03f;

            float separationWeight = 0.15f;
            float alignmentWeight = 0.02f;
            float cohesionWeight = 0.003f;

            int maxSeparationNeighbords = 20;
            int maxAlignmentNeighbords = 10;
            int maxCohesionNeighbords = 10;


            Boid currBoid = Boids[i];

            // Update boid
            Vector3 separation = Vector3.zero;
            Vector3 alignment = Vector3.zero;
            Vector3 cohesion = Vector3.zero;


            int sepCount = 0;
            int aliCount = 0;
            int cohCount =0;

            for (int j = 0; j < BoidCount; j++)
            {
                if (i == j ) continue;
                {
                    Boid other = Boids[j];
                    float dist = Vector3.Distance(currBoid.Position, other.Position);

                    if (dist < neigborRadius)
                    {

                        // Separation
                        if(dist < separationRadius && sepCount < maxSeparationNeighbords)
                        {
                            separation += (currBoid.Position - other.Position).normalized * (separationRadius - dist) / separationRadius;
                            sepCount++;
                        }

                        //Alignment
                        if(dist <neigborRadius && aliCount < maxAlignmentNeighbords)
                        {
                            alignment += other.Velocity;
                            aliCount++;
                        }

                        //Cohesion
                        if (dist < neigborRadius && cohCount < maxCohesionNeighbords)
                        {
                            cohesion += other.Position;
                            cohCount++;
                        }

                    }
 

                }


            }

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

            Vector3 force = separationWeight * separation
                                + alignmentWeight * alignment
                                + cohesionWeight * cohesion;


            //limit force 
            if (force.magnitude > maxForce)
                force = force.normalized * maxForce;


            // update velocity 
            currBoid.Velocity += force;
            if (currBoid.Velocity.magnitude > maxSpeed)
                currBoid.Velocity = currBoid.Velocity.normalized * maxSpeed;

            //update pos
            currBoid.Position += currBoid.Velocity * Time.deltaTime;


            //synchronize GameObject
            currBoid.Object.transform.position = currBoid.Position;
            if (currBoid.Velocity != Vector3.zero)
                currBoid.Object.transform.rotation = Quaternion.LookRotation(currBoid.Velocity);

            // save the boid update 
            Boids[i] = currBoid;
        }
    }
}
