using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YongSeok
{

    public class EventTester : MonoBehaviour
    {
        [SerializeField] private HeroBase heroToTest;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (heroToTest != null)
                {
                    heroToTest.TestDamage(30); // 데미지 입힘
                }
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                if (heroToTest != null)
                {
                    heroToTest.TestStageChange(); // 스테이지 이동 테스트
                }
            }
        }
    }

}