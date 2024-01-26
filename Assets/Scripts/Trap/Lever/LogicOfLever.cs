using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicOfLever : MonoBehaviour
{
    private Animator anim;
    [SerializeField]private bool isPlayerInRange;
    private PlayerItemHandler playerItem;
    [SerializeField] private bool stateOfLever = false;
    public LDoor lDoor;
    void Start()
    { 
       // anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(InputManager.instance._interactL && isPlayerInRange)
        {
            Debug.Log("PullDownTheLever_E");
            if(!stateOfLever)
            {
                stateOfLever = true;
                lDoor.GetComponent<DoorControledByLever>().lState = stateOfLever;
                //anim.SetBool("turn",stateOfLever);
            }
            else
            {
                stateOfLever = false;
                lDoor.GetComponent<DoorControledByLever>().lState = stateOfLever;
                //anim.SetBool("turn",stateOfLever);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            playerItem = other.GetComponent<PlayerItemHandler>();
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
    

    public void OnEnd()
    {
        gameObject.SetActive(false);
    }
}
