using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPassHandler : MonoBehaviour
{
    [SerializeField]private bool hasPass;
    [SerializeField]private List<Pass> pass;
    [SerializeField] private int passNum;
    [SerializeField] private int nowNum;
    public void Init()
    {
        hasPass = false;
        nowNum = 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Pass>() && !hasPass)
        {
            GetPass(other.GetComponent<Pass>());
            //other.GetComponent<Pass>().GetThisOne();
        }
    }

    public void GetPass(Pass pass)
    {
        hasPass = true;
        this.pass[nowNum++] = pass;
    }

    public void UsePass()
    {
        hasPass = false;
    }
 
    public bool CheckHasKey()
    {
        return hasPass;
    }

    // public bool ComparePass(Pass pass)
    // {
    //     //return this.pass == pass;
    // }
}