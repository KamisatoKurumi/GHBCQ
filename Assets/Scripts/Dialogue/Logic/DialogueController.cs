using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Farm.Dialogue
{
    // [RequireComponent(typeof(NPCMovement))]
    // [RequireComponent(typeof(BoxCollider2D))]
    public class DialogueController : MonoBehaviour
    {
        // private NPCMovement npc => GetComponent<NPCMovement>();
        public UnityEvent OnFinishEvent;
        //能触发这条对话的对象
        public PlayerType playerType;
        public List<DialoguePiece> dialogueList_PlayerA = new List<DialoguePiece>();
        public List<DialoguePiece> dialogueList_PlayerB = new List<DialoguePiece>();
        public List<DialoguePiece> dialogueList_Both = new List<DialoguePiece>();
        private List<DialoguePiece> dialogueList = new List<DialoguePiece>();

        private Stack<DialoguePiece> dialogueStack;
        public bool canActivate;

        private bool isSpeeking =false;

        public bool canTalk;
        private bool isTalking;
        [SerializeField]private GameObject uiSign;

        private void Awake() {
            if(transform.childCount > 0)
            {
                uiSign = transform.GetChild(0).gameObject;
            }
            FillDialogueStack();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if(canActivate)
            {
                if(other.CompareTag("Player") && (other.GetComponent<PlayerTag>()._tag == playerType || playerType == PlayerType.Both))
                {
                    canTalk = true;
                    dialogueList = other.GetComponent<PlayerTag>()._tag == PlayerType.Both ? dialogueList_Both : other.GetComponent<PlayerTag>()._tag == PlayerType.A ? dialogueList_PlayerA : dialogueList_PlayerB;
                    FillDialogueStack();
                }
            }
            else
            {
                if(!isSpeeking && other.CompareTag("Player"))
                {
                    isSpeeking = true;
                    StartCoroutine(DialogueRoutine());
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if(canActivate)
            {
                if(other.CompareTag("Player"))
                {
                    canTalk = false;
                }
            }
        }
        public void SetDialogueList()
        {
            dialogueList = dialogueList_Both;
            FillDialogueStack();
        }

        private void Update() {
            if(canActivate)
            {
                if(uiSign != null)
                    uiSign.SetActive(canTalk);

                if(canTalk & InputManager.instance._interact)
                {
                    StartCoroutine(DialogueRoutine());
                }
            }
            else
            {
                if(isSpeeking && InputManager.instance._interact)
                {
                    StartCoroutine(DialogueRoutine());
                }
            }
        }

        //构建对话堆栈
        private void FillDialogueStack()
        {
            dialogueStack = new Stack<DialoguePiece>();
            for(int i = dialogueList.Count - 1; i > -1; i--)
            {
                dialogueList[i].isDone = false;
                dialogueStack.Push(dialogueList[i]);
            }
        }

        public IEnumerator DialogueRoutine()
        {
            isTalking = true;
            if(dialogueStack.TryPop(out DialoguePiece result))
            {
                //传到UI显示对话
                DialogueManager.CallShowDialogueEvent(result);
                // EventHandler.CallUpdateGameStateEvent(GameState.Pause);
                yield return new WaitUntil(() => result.isDone);
                isTalking = false;
            }
            else
            {
                // EventHandler.CallUpdateGameStateEvent(GameState.GamePlay);
                DialogueManager.CallShowDialogueEvent(null);
                FillDialogueStack();
                isTalking = false;

                if(OnFinishEvent != null)
                {
                    OnFinishEvent.Invoke();
                    canTalk = false;
                }
            }
        }

    }
}
