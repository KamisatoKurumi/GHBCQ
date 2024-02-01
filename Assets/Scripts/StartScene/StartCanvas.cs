using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCanvas : EveryScene_Main
{
    [SerializeField]private GameObject startPanel;
    [SerializeField]private GameObject LevelPanel;
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
        startPanel.SetActive(true);
        LevelPanel.SetActive(false);
    }

    public void Start2Level()
    {
        startPanel.SetActive(false);
        LevelPanel.SetActive(true);
        InitLevelPanel();
    }

    public void InitLevelPanel()
    {
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
}
