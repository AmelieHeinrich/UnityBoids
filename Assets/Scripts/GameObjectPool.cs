using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool
{
    private Queue<GameObject> Objects;
    private GameObject Prefab;
    private Transform Parent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObjectPool(GameObject prefab, int initialSize, Transform parent = null)
    {
        Objects = new Queue<GameObject>();

        this.Prefab = prefab;
        if (parent != null)
        {
            this.Parent = parent;
        }

        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = GameObject.Instantiate(prefab, parent);
            obj.SetActive(false);
            Objects.Enqueue(obj);
        }
    }

    public GameObject Get()
    {
        if (Objects.Count > 0)
        {
            var obj = Objects.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        // Expand if necessary
        return GameObject.Instantiate(Prefab, Parent);
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        Objects.Enqueue(obj);
    }
}
