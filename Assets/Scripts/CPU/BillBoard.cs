using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public Camera MainCamera;

    void Start()
    {
        // if no camera assigned, use the main camera 
        if (MainCamera == null)
        {
            MainCamera = Camera.main;   
        }
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.forward = MainCamera.transform.forward;  
    }
}
