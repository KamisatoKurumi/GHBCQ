using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPass : MonoBehaviour
{
    [SerializeField] private List<Pass> pass1;
    [SerializeField] private List<Pass> pass2;
    [SerializeField] private int passType;
    [SerializeField] private List<bool> qualified;

    private void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            qualified[i] = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        bool count = false;
        if (other.CompareTag("Player") && other.GetComponent<PlayerTag>()._tag == PlayerType.A)
        {
            for (int i = 0; i < passType; i++)
            {
                if (!pass1[i].GetComponent<LogicOfPass>().HasThisPass(0) && !pass2[i].GetComponent<LogicOfPass>().HasThisPass(0)) count = true;
            }

            if (!count) qualified[0] = true;
        }
        else if (other.CompareTag("Player") && other.GetComponent<PlayerTag>()._tag == PlayerType.B)
        {
            for (int i = 0; i < passType; i++)
            {
                if (!pass1[i].GetComponent<LogicOfPass>().HasThisPass(1) && !pass2[i].GetComponent<LogicOfPass>().HasThisPass(1)) count = true;
            }

            if (!count) qualified[1] = true;
        }
    }
    

    public bool IsQualified(int playerNum)
    {
        return qualified[playerNum];
    }
    
    public void EnterDoor(int playerNum)
    {
        qualified[playerNum] = false;
    }
}
