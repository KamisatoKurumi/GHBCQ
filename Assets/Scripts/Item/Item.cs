using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string _name;
    public string _tag;

    public void GetThisOne()
    {
        gameObject.SetActive(false);
    }
}   
