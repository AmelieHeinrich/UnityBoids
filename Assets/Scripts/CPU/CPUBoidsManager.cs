using System.Collections.Generic;
using UnityEngine;

public class CPUBoidsManager : MonoBehaviour
{
    struct Boid
    {
        public Vector3 Position;
        public Vector3 Velocity;
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
        Vector3 randomVelocity = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        for (int i = 0; i < BoidCount; i++)
        {
            Boid boid = new Boid();
            boid.Position = this.gameObject.transform.position + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            boid.Velocity = randomVelocity;
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
            // Select surrounding neighbours?
            // Separation
            // Alignment
            // Cohesion
        }
    }
}
