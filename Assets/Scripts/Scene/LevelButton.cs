using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI text;
    private bool isUnlocked = false;
    public void Init(int levelIndex)
    {
        text.text = "Level " + levelIndex;
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            TransitionManager.instance.EnterLevel(levelIndex - 1);
        });
        SetUnlocked(levelIndex <= TransitionManager.instance.currentLevel);
        Debug.Log("LevelButton Init");
    }
    public void SetUnlocked(bool unlocked)
    {
        isUnlocked = unlocked;
        GetComponent<Button>().interactable = isUnlocked;
        if(isUnlocked)
        {
            GetComponent<Image>().color = Color.white;
        }
        else
        {
            GetComponent<Image>().color = Color.gray;
        }
    }
}
