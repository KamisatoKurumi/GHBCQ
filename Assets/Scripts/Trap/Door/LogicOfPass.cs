using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicOfPass : MonoBehaviour
{
    [SerializeField]private List<bool> hasPass;
    [SerializeField] private Pass brohter;
    private void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            hasPass[i] = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerTag>()._tag == PlayerType.A && brohter.GetComponent<LogicOfPass>().hasPass[0] == false)
        {
            hasPass[0] = true; 
            GetPass();
        }
        else if (other.CompareTag("Player") && other.GetComponent<PlayerTag>()._tag == PlayerType.B &&  brohter.GetComponent<LogicOfPass>().hasPass[1] == false)
        { 
            hasPass[1] = true;
            GetPass();
        }
    }

    public bool HasThisPass(int playerNum)
    {
        return hasPass[playerNum];
    }

    public void GetPass()
    {
        gameObject.SetActive(false);
    }
    
}
