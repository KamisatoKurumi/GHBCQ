using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicOfPass : MonoBehaviour
{
    [SerializeField]private List<bool> hasPass;

    private void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            hasPass[i] = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerTag>()._tag == PlayerType.A)
            hasPass[0] = true;
        else if (other.CompareTag("Player") && other.GetComponent<PlayerTag>()._tag == PlayerType.B)
        {
            hasPass[1] = true;
        }
    }

    public bool HasThisPass(int playerNum)
    {
        return hasPass[playerNum];
    }
    
}
