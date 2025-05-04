using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimator : MonoBehaviour
{
    [SerializeField] public GameObject[] prefab;    // ¿ÜÇü

    private void OnEnable()
    {
        int shape = 0;
        if (gameObject.GetComponent<MonsterStatus>().boss) shape = 0;
        else
        {
            if (GameManager.Instance.player.Stage < 7)
                shape = 0;
            else if (GameManager.Instance.player.Stage < 14)
                shape = 1;
            else
                shape = 2;
        }

        GameObject monster = Instantiate(prefab[shape], new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 180, 0)));
        monster.AddComponent<MonsterStatus>();
        monster.GetComponent<MonsterStatus>().SetStatus(gameObject.GetComponent<MonsterStatus>());
        monster.name = name;
    }
}
