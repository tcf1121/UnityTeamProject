using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //private SceneChanger scene;
    //public SceneChanger Scene { get { return scene; } }
    
    public Player player;

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [Header("UI")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private Button startBtn;
    [SerializeField] private Button endBtn;

    private void Awake()
    {
        SetSingleton();
    }
    private void SetSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        startBtn.onClick.AddListener(GameStart);
        endBtn.onClick.AddListener(EndGame);
    }

    private void GameStart()
    {
        //플레이어 활성화
        //player.setPlayer();
        playerObject.SetActive(true);
        // UI 비활성화
        startPanel.SetActive(false);
    }


    private void GameOver()
    {

    }

    private void EndGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                        Application.Quit();
        #endif
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.Gold++;
        }
    }
}
