using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI text;
    public void Init(int levelIndex)
    {
        text.text = "Level " + levelIndex;
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            TransitionManager.instance.EnterLevel(levelIndex - 1);
        });
        Debug.Log("LevelButton Init");
    }
}
