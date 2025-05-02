using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITarget
{
    // 아군, 적군 공통으로 공격모션을 사용하기 위해 인터페이스 사용

    public void TakeDamage(int damage);

}
