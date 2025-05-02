using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] new Camera camera = new();
    [SerializeField] new Camera overUIcamera = new();
    void Start()
    {
        camera.transform.position = new Vector3(-6, 10, -1);
        camera.transform.rotation = Quaternion.Euler(new Vector3(50, 0, 0));
        overUIcamera.transform.position = new Vector3(-6, 10, -1);
        overUIcamera.transform.rotation = Quaternion.Euler(new Vector3(50, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
