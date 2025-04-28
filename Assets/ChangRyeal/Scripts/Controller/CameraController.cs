using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Camera camera;
    void Start()
    {
        camera.transform.position = new Vector3(0, 5, -8);
        camera.transform.rotation = Quaternion.Euler(new Vector3(45, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
