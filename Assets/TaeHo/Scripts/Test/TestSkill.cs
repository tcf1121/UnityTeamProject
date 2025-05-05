using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkill : MonoBehaviour
{
    public void ActivateSkill()
    {
        Debug.Log($"{gameObject.name}이 스킬을 발동했다!");
    }
}
