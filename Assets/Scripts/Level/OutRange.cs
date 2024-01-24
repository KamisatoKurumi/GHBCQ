using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutRange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player Out Range");
            GameFlowManager.instance.ResetPlayer();
        }
    }
}
