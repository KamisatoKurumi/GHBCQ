using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewDoor : MonoBehaviour
{
    [SerializeField]private Key correctKey;
    private Animator anim;

    private bool isPlayerInRange;
    private PlayerItemHandler playerItem;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(InputManager.instance._interact && isPlayerInRange)
        {
            Debug.Log("GetKeyDown_E");
            if(playerItem.CheckHasKey() && playerItem.CompareKey(correctKey))
            {
                OpenDoor(); 
                playerItem.UseKey();
            }
            else
            {
                Debug.Log("没有对应钥匙");
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

    // void OnTriggerStay2D(Collider2D other)
    // {
    //     if(other.CompareTag("Player"))
    //     {
    //         Debug.Log("Tag is Player");
    //         if(Input.GetKeyDown(KeyCode.E))
    //         {
    //             Debug.Log("GetKeyDown_E");
    //             PlayerItemHandler player = other.GetComponent<PlayerItemHandler>();
    //             if(player.CheckHasKey() && player.CompareKey(correctKey))
    //             {
    //                 OpenDoor(); 
    //                 player.UseKey();
    //             }
    //             else
    //             {
    //                 Debug.Log("没有对应钥匙");
    //             }
    //         }
    //     }
    // }

    private void OpenDoor()
    {
        Debug.Log("Open");
        anim.SetTrigger("Open");
    }

    public void OnEnd()
    {
        gameObject.SetActive(false);
    }
}
