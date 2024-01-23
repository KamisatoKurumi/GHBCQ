using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]private PlayerType correctTpye;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && other.GetComponent<PlayerTag>()._tag == correctTpye)
        {
            Debug.Log("CheckPoint");
            GameFlowManager.instance.PassLevel();
        }
    }
}
