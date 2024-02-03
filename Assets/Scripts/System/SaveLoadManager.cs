using UnityEngine;
using System.IO;

[System.Serializable]
public class LevelData
{
    public int levelNumber;
    public bool isPassed;

    public LevelData()
    {
        // 默认构造函数
    }
    public LevelData(int levelNumber, bool isPassed)
    {
        this.levelNumber = levelNumber;
        this.isPassed = isPassed;
    }

    // 可以根据需要添加其他关卡相关的信息
}

public class SaveLoadManager : MonoSingleton<SaveLoadManager>
{
    // 存档文件的路径
    private string saveFilePath;

    void Start()
    {
        // 设置存档文件路径（可根据实际情况修改）
        saveFilePath = Path.Combine(Application.persistentDataPath, "levelData.json");

        // 调用保存和加载方法的示例
        // SaveLevelData(new LevelData { levelNumber = 1, isPassed = true});
        // LevelData loadedData = LoadLevelData();

        // 在这里使用 loadedData 处理加载到的数据
        // Debug.Log("Level 1 Passed: " + loadedData.isPassed);
    }

    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.F2))
        // {
        //     SaveLevelData(new LevelData { levelNumber = 2, isPassed = true});
        // }
        // if(Input.GetKeyDown(KeyCode.F3))
        // {
        //     LevelData loadedData = LoadLevelData();
        //     Debug.Log("Level 2 Passed: " + loadedData.isPassed);
        // }
    }

    public void SaveLevelData(LevelData data)
    {
        Debug.Log("SaveLevelData");
        // 将 LevelData 对象序列化为 JSON 字符串
        string json = JsonUtility.ToJson(data);

        // 将 JSON 字符串写入本地文件
        File.WriteAllText(saveFilePath, json);
    }

    public LevelData LoadLevelData()
    {
        // 如果存档文件不存在，返回默认的 LevelData
        if (!File.Exists(saveFilePath))
        {
            Debug.Log("File Not Exist");
            return new LevelData { levelNumber = 1, isPassed = false };
        }

        // 从本地文件读取 JSON 字符串
        string json = File.ReadAllText(saveFilePath);

        // 将 JSON 字符串反序列化为 LevelData 对象
        LevelData loadedData = JsonUtility.FromJson<LevelData>(json);

        // 返回加载到的 LevelData 对象
        return loadedData;
    }
}