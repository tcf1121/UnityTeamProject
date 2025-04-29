using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void Load(int sceneNumber)
    {
        GameManager.Instance.Scene.Load(sceneNumber);
    }

    public void Load(string sceneName)
    {
        GameManager.Instance.Scene.Load(sceneName);
    }
}
