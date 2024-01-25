using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScene : EveryScene_Main
{
    [SerializeField]private GameObject finalUI;
    [SerializeField]private GameObject stopUI;
    void OnEnable()
    {
        GameFlowManager.OnGameOver += OnGameOver;
        GameFlowManager.OnPauseGame += OnPauseGame;
        GameFlowManager.OnContinueGame += OnContinueGame;
    }

    void OnDisable()
    {
        GameFlowManager.OnGameOver -= OnGameOver;
        GameFlowManager.OnPauseGame -= OnPauseGame;
        GameFlowManager.OnContinueGame -= OnContinueGame;
    }

    void OnGameOver()
    {
        Time.timeScale = 0;
        finalUI.SetActive(true);
    }

    void OnPauseGame()
    {
        stopUI.SetActive(true);
    }

    void OnContinueGame()
    {
        stopUI.SetActive(false);
    }

    public override void Init()
    {
        Time.timeScale = 1;
        GameFlowManager.instance.InitLevel();
        finalUI.SetActive(false);
        stopUI.SetActive(false);
    }

    public void OnNextLevelButton()
    {
        Debug.Log("OnNextLevelButton");
        TransitionManager.instance.EnterLevel(TransitionManager.instance.currentLevel + 1);
    }

    public void OnBack2StartPanelButton()
    {
        Debug.Log("OnBack2StartPanelButton");
        TransitionManager.instance.EnterStartScene();
    }
}
