using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] new Camera camera = new();
    void Start()
    {
        camera.transform.position = new Vector3(-6, 10, -4);
        camera.transform.rotation = Quaternion.Euler(new Vector3(45, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
