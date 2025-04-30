using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoController : MonoBehaviour
{
    [SerializeField] GameObject info;
    void Start()
    {
        info.SetActive(true);
    }

}
