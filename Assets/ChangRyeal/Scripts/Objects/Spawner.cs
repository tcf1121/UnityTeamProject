using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<GameObject> Monsters;

    private void OnEnable()
    {
        Respawn(0);
    }

    public void Respawn(int monNum)
    {
        int shape = 0;
        if (Monsters[monNum].GetComponent<MonsterStatus>().boss) shape = 0;
        else
        {
            if (GameManager.Instance.player.Stage < 7)
                shape = 0;
            else if (GameManager.Instance.player.Stage < 14)
                shape = 1;
            else
                shape = 2;
        }
        GameObject monster = Instantiate(Monsters[monNum].GetComponent<MonsterStatus>().prefab[shape], new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 180, 0)));
        monster.AddComponent<MonsterStatus>();
        monster.GetComponent<MonsterStatus>().SetStatus(Monsters[monNum].GetComponent<MonsterStatus>());
        monster.name = Monsters[monNum].name;
    }
}
