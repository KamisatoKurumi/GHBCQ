using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoSingleton<GameFlowManager>
{
    public GameObject[] _players;
    private Player[] _playerScripts;

    [SerializeField]private Transform[] gameStartPoints = new Transform[2];

    [SerializeField]private int currentPlayer;

    private bool isPause = false;

    public static event Action OnInitLevel;
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

    public void InitLevel()
    {
        OnInitLevel?.Invoke();
        
        AudioManager.PlayAudio(AudioName.BGM_1);
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
    }

    public void SetPlayerInStartPoint()
    {
        currentPlayer = 0;
        ResetPlayer();
    }

    public void ResetPlayer()
    {
        _players[currentPlayer].SetActive(true);
        _players[currentPlayer].transform.position = gameStartPoints[currentPlayer].position;
        foreach(var player in _players)
        {
            if(player != _players[currentPlayer])
            {
                player.SetActive(false);
            }
        }
    }

    public void ResetCurrentLevel()
    {
        for (int i = 0; i < _players[currentPlayer].GetComponentInChildren<PlayerTag>().hadKeys.Count; i++)
        {
            _players[currentPlayer].GetComponentInChildren<PlayerTag>().hadKeys[i] = false;
        }
        TransitionManager.instance.ResetCurrentLevel();
    }

    public void PauseGame()
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
        SaveLoadManager.instance.SaveLevelData(new LevelData(TransitionManager.instance.currentLevel, true));
    }

    public void LockPlayers()
    {
        Debug.Log("LockPlayers");
        foreach(var player in _players)
        {
            player.GetComponent<PlayerController>()._active = false;
        }
    }

    public void UnlockPlayers()
    {
        Debug.Log("UnlockPlayers");
        foreach(var player in _players)
        {
            player.GetComponent<PlayerController>()._active = true;
        }
    }
}
