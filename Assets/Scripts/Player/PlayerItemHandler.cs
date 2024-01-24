using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerItemHandler : MonoBehaviour
{
    [SerializeField]private bool hasKey;
    [SerializeField]private Key key;
    public void Init()
    {
        hasKey = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Key>() && !hasKey)
        {
            GetKey(other.GetComponent<Key>());
            other.GetComponent<Key>().GetThisOne();
        }
    }

    public void GetKey(Key key)
    {
        hasKey = true;
        this.key = key;
    }

    public void UseKey()
    {
        hasKey = false;
        Destroy(this.key.gameObject);
        this.key = null;
    }

    public bool CheckHasKey()
    {
        return hasKey;
    }

    public bool CompareKey(Key key)
    {
        return this.key == key;
    }
}
