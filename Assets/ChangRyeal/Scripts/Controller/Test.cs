using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            GameManager.Instance.player.Health--;
        if (Input.GetKeyDown(KeyCode.W))
            GameManager.Instance.player.Gold++;
        if (Input.GetKeyDown(KeyCode.E))
            GameManager.Instance.player.Expplus();
        //if (Input.GetKeyDown(KeyCode.R))
        //    GameManager.Instance.player.Health--;
        //if (Input.GetKeyDown(KeyCode.Q))
        //    GameManager.Instance.player.Health--;
    }
}
