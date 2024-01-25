using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GameFlowManager : MonoSingleton<GameFlowManager>
{
    [SerializeField]private GameObject[] _players;
    private Player[] _playerScripts;

    [SerializeField]private Transform[] gameStartPoints = new Transform[2];

    [SerializeField]private int currentPlayer;

    private bool isPause = false;

    public static event Action OnGameOver;
    public static event Action OnPauseGame;
    public static event Action OnContinueGame;

    void Start()
    {
        _playerScripts = new Player[_players.Length];
        for(int i = 0;i < _players.Length; ++i)
        {
            _playerScripts[i] = _players[i].GetComponent<Player>();
        }
    }

    void OnEnable()
    {
        GameFlowManager.OnGameOver += OnGameOverEvent;
        TransitionManager.BeforeEnterStartScene += OnBeforeEnterStartScene;
    }

    void OnDisable()    
    {
        GameFlowManager.OnGameOver -= OnGameOverEvent;
        TransitionManager.BeforeEnterStartScene -= OnBeforeEnterStartScene;
    }

    void Update()
    {
        if(InputManager.instance._resetGame)
        {
            ResetCurrentLevel();
        }
        if(InputManager.instance._stopGame)
        {
            if(isPause)
            {
                OnContinueGame?.Invoke();
                Time.timeScale = 1;
                isPause = false;
            }
            else
            {
                OnPauseGame?.Invoke();
                Time.timeScale = 0;
                isPause = true;
            }
        }
    }

    public void InitLevel()
    {
        Transform startPoint = GameObject.Find("StartPoint").transform;
        if(startPoint.childCount <= 0)
        {
            gameStartPoints[0] = startPoint;
            gameStartPoints[1] = startPoint;
        }
        else
        {
            for(int i = 0;i <= 1; ++i)
            {
                gameStartPoints[i] = GameObject.Find("StartPoint").transform.GetChild(i).transform;
            }
        }
        foreach(Player player in _playerScripts)
        {
            player.Init();
        }
        currentPlayer = 0;
        ResetPlayer();
        _players[1].SetActive(false);
    }

    public void ResetPlayer()
    {
        _players[currentPlayer].SetActive(true);
        _players[currentPlayer].transform.position = gameStartPoints[currentPlayer].position;
    }

    private void ResetCurrentLevel()
    {
        TransitionManager.instance.ResetCurrentLevel();
    }

    private void OnBeforeEnterStartScene()
    {
        foreach(var player in _players)
        {
            player.SetActive(false);
        }
    }

    public void PassLevel()
    {
        // Debug.Log("PassLevel");
        if(currentPlayer == 0)
        {
            currentPlayer = 1;
            ResetPlayer();
            _players[0].SetActive(false);
        }
        else
        {
            // Debug.Log("Game Over");
            OnGameOver?.Invoke();
        }
    }

    void OnGameOverEvent()
    {
        foreach(GameObject player in _players)
        {
            player.SetActive(false);
        }
    }
}
