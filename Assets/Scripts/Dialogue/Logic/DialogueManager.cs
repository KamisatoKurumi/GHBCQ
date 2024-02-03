using System;
using System.Collections;
using System.Collections.Generic;
using Farm.Dialogue;
using UnityEngine;

public class DialogueManager : MonoSingleton<DialogueManager>
{
    public static event Action<DialoguePiece> ShowDialogueEvent;
    public static void CallShowDialogueEvent(DialoguePiece piece)
    {
        ShowDialogueEvent?.Invoke(piece);
    }
    public static event Action OnDialogueOpenEvent;
    public static void CallOnDialogueOpenEvent()
    {
        OnDialogueOpenEvent?.Invoke();
    }
    public static event Action OnDialogueCloseEvent;
    public static void CallOnDialogueCloseEvent()
    {
        OnDialogueCloseEvent?.Invoke();
    }
}
