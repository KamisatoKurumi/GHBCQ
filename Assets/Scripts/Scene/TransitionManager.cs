using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoSingleton<TransitionManager>
{
    [SceneName]
    [SerializeField]private string _startScene;
    void Start()
    {
        Init();
    }

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
    }


    private IEnumerator LoadSceneSetActive(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        
        SceneManager.SetActiveScene(newScene);
    }
}
