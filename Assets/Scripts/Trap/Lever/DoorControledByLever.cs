using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorControledByLever : MonoBehaviour
{
    public Animator anim;
    public bool lState;
    public Vector3 trans1;
    public Vector3 trans2;
    public float moveSpeed;
    void Start()
    {
        //anim = GetComponent<Animator>();
        trans1 = transform.position;
        trans2 = transform.position + new Vector3(0,5,0);
    }

    private void FixedUpdate()
    {
        //SetAnim();
        if (lState) transform.position = Vector3.Lerp(transform.position, trans2, moveSpeed *Time.deltaTime);
        else transform.position = Vector3.Lerp(transform.position, trans1, moveSpeed *Time.deltaTime);
        
    }

    // public void SetAnim()
    // {
    //     anim.SetBool("LState",lState);
    // }
    
}
