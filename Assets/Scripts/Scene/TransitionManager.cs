using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoSingleton<TransitionManager>
{
    [SceneName]
    [SerializeField]private string _startScene;

    [SerializeField]public string[] _levelScenes;

    public int currentLevel;

    public static event Action BeforeEnterStartScene;
    void Start()
    {
        Init();
    }

    //启动游戏时初始化
    private void Init()
    {
        Scene[] loadedScenes = new Scene[SceneManager.sceneCount];
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            loadedScenes[i] = SceneManager.GetSceneAt(i);
        }

        // 遍历每个加载的场景，如果不是 Persistent Scene 则卸载它
        foreach (Scene scene in loadedScenes)
        {
            if (scene.name != "PersistentScene") // 替换为你的 Persistent Scene 的名字
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }
        StartCoroutine(LoadSceneSetActive(_startScene));
        SavaDataWhenSceneTransition();
    }

    public void SavaDataWhenSceneTransition()
    {
        LevelData levelData = SaveLoadManager.instance.LoadLevelData();
        if(levelData.isPassed)
            currentLevel = levelData.levelNumber + 1;
        else
            currentLevel = levelData.levelNumber;
    }

    public void EnterLevel(int levelIndex)
    {
        if(levelIndex >= _levelScenes.Length)
        {
            EnterStartScene();
            return;
        }
        StartCoroutine(Transition(_levelScenes[levelIndex]));
        currentLevel = levelIndex + 1;
    }

    public void EnterStartScene()
    {
        BeforeEnterStartScene?.Invoke();
        StartCoroutine(Transition(_startScene));
    }

    public void ResetCurrentLevel()
    {
        StartCoroutine(Transition(_levelScenes[currentLevel - 1]));
    }
    private IEnumerator Transition(string sceneName)
    {
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        
        yield return LoadSceneSetActive(sceneName);
    }


    private IEnumerator LoadSceneSetActive(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        
        SceneManager.SetActiveScene(newScene);

        InitCurrentScene();
    }
    //在加载每个场景过后初始化每个场景
    private void InitCurrentScene()
    {
        EveryScene_Main[] everySceneMains = FindObjectsOfType<EveryScene_Main>();
        foreach (EveryScene_Main everySceneMain in everySceneMains)
        {
            everySceneMain.Init();
        }
    }


}
