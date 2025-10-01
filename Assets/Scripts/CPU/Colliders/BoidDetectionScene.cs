using System.Collections.Generic;
using UnityEngine;

public class BoidDetectionScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public List<ICollider> Colliders = new List<ICollider>();
}
