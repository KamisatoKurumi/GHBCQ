using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCanvas : EveryScene_Main
{
    [SerializeField]private GameObject startPanel;
    [SerializeField]private GameObject LevelPanel;
    [SerializeField]private GameObject settingPanel;
    // [SerializeField]private Button startButton;

    [SerializeField]private GameObject levelHandler;
    [SerializeField]private GameObject levelButtonPrefab;

    void Update()
    {
        if(InputManager.instance._stopGame)
        {
            Init();
        }
    }

    public override void Init()
    {
        GameFlowManager.instance.LockPlayers();
        startPanel.SetActive(true);
        LevelPanel.SetActive(false);
        settingPanel.SetActive(false);
    }

    public void Start2Level()
    {
        startPanel.SetActive(false);
        LevelPanel.SetActive(true);
        InitLevelPanel();
    }

    public void InitLevelPanel()
    {
        TransitionManager.instance.SavaDataWhenSceneTransition();
        //Clear All Children
        foreach (Transform child in levelHandler.transform)
        {
            Destroy(child.gameObject);
        }
        string[] levelScenes = TransitionManager.instance._levelScenes;
        for (int i = 0; i < levelScenes.Length; i++)
        {
            GameObject levelButton = Instantiate(levelButtonPrefab, levelHandler.transform);
            levelButton.GetComponent<LevelButton>().Init(i + 1);
        }
    }

    public void OnSetButtonClicked()
    {
        startPanel.SetActive(false);
        settingPanel.SetActive(true);
    }

    public void OnQuitButtonClicked()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}
