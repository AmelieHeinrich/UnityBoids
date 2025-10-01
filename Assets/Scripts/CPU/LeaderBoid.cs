using System.Collections;
using UnityEngine;

public class LeaderBoid : MonoBehaviour
{
    [SerializeField] public LimitArea BoidLimitArea;
    [SerializeField] private float speed = 2f; 
    [SerializeField] public Vector3 targetPosition;
    [SerializeField] public BoidDetectionScene BoidScene;

    void Start()
    {
        SetRandomTarget();
    }

    // Update is called once per frame
    void Update()
    {
        float targetDistance = Vector3.Distance(transform.position, targetPosition);
        Debug.Log(targetDistance);
        bool inRadiusOfTarget = targetDistance < 5f;
        if (inRadiusOfTarget)
            SetRandomTarget();

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        Vector3 repulsion = BoidLimitArea.GetRepulsionForce(transform.position);
        transform.position += repulsion * Time.deltaTime;   
    }

    private void SetRandomTarget()
    {
        foreach (ICollider collider in BoidScene.Colliders)
        {
            Vector3 newPos = BoidLimitArea.transform.position;
            while (collider.DistanceWithSphere(newPos, 5f) < 1.0f)
            {
                newPos = BoidLimitArea.transform.position + Random.insideUnitSphere * Random.Range(1.0f, BoidLimitArea.radius);
            }
            targetPosition = newPos;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetPosition, 5f);
    }
}
