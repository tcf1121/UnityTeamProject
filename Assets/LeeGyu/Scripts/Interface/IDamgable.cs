using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamgable
{
    public void TakeDamage(GameObject gameObject, float attackPower);
}
