using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private SceneChanger scene;
    public SceneChanger Scene { get { return scene; } }

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
        // �׽�Ʈ�� �߰�
        testGameStart();
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
        //�÷��̾� Ȱ��ȭ
        //player.setPlayer();
        
        SceneManager.LoadScene(1);
    }


    private void testGameStart()
    {
        //�÷��̾� Ȱ��ȭ
        playerObject.SetActive(true);
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
