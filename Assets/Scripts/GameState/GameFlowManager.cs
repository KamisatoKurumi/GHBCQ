using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoSingleton<GameFlowManager>
{
    [SerializeField]private GameObject[] _players;

    [SerializeField]private Transform gameStartPoint;

    [SerializeField]private int currentPlayer;

    public static event Action OnGameOver;

    void Awake()
    {
        base.Awake();
        gameStartPoint = GameObject.Find("StartPoint").transform;
    }

    void Start()
    {
        InitLevel();
    }

    void InitLevel()
    {
        currentPlayer = 0;
        ResetPlayer();
        _players[1].SetActive(false);
    }

    public void ResetPlayer()
    {
        _players[currentPlayer].SetActive(true);
        _players[currentPlayer].transform.position = gameStartPoint.position;
    }

    public void PassLevel()
    {
        Debug.Log("PassLevel");
        if(currentPlayer == 0)
        {
            currentPlayer = 1;
            ResetPlayer();
            _players[0].SetActive(false);
        }
        else
        {
            Debug.Log("Game Over");
            OnGameOver?.Invoke();
        }
    }
}
