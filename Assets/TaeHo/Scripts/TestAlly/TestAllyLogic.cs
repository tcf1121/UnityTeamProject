using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAllyLogic : MonoBehaviour
{

    [SerializeField] GameObject ally;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            ally.SetActive(false);
        }
    }
}
