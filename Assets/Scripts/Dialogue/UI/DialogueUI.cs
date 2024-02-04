using System.Collections;
using System.Collections.Generic;
using Farm.Dialogue;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public Image faceRight, faceLeft;



    private void Awake() {
        dialogueBox.SetActive(false);
    }

    private void OnEnable() {
        DialogueManager.ShowDialogueEvent += OnShowDialogueEvent;   
    }


    private void OnDisable() {
        DialogueManager.ShowDialogueEvent -= OnShowDialogueEvent;
    }
    private void OnShowDialogueEvent(DialoguePiece piece)
    {
        StartCoroutine(ShowDialogue(piece));
    }

    private IEnumerator ShowDialogue(DialoguePiece piece)
    {
        if(piece != null)
        {
            DialogueManager.CallOnDialogueOpenEvent();
            piece.isDone = false;

            dialogueBox.SetActive(true);

            dialogueText.text = string.Empty;

            if(piece.name != string.Empty)
            {
                if(piece.onLeft)
                {
                    faceRight.gameObject.SetActive(false);
                    faceLeft.gameObject.SetActive(true);
                    faceLeft.sprite = piece.faceImage;
                }
                else
                {
                    faceRight.gameObject.SetActive(true);
                    faceLeft.gameObject.SetActive(false);
                    faceRight.sprite = piece.faceImage;
                }
            }
            else
            {
                faceLeft.gameObject.SetActive(false);
                faceRight.gameObject.SetActive(false);
            }
            yield return dialogueText.DOText(piece.dialogueText, 1f).WaitForCompletion();

            piece.isDone = true;

        }
        else
        {
            DialogueManager.CallOnDialogueCloseEvent();
            dialogueBox.SetActive(false);
            yield break;
        }
    }

}
