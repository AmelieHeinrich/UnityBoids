using System.Collections;
using UnityEngine;

public class LeaderBoid : MonoBehaviour
{
    private LimitArea BoidLimitArea;

    [SerializeField] private float speed = 2f; 
    [SerializeField] private float Changetimer = 3f;

    [HideInInspector] public Vector3 targetPosition;

    private float timer;

    void Start()
    {
        SetRandomTarget();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= Changetimer)
        {
            timer = 0f;
            SetRandomTarget();
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        Vector3 repulsion = BoidLimitArea.GetRepulsionForce(transform.position);
        transform.position += repulsion * Time.deltaTime;   
    }

    private void SetRandomTarget()
    {
        targetPosition = BoidLimitArea.transform.position + Random.insideUnitSphere * BoidLimitArea.radius;
    }
}
