using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoSingleton<InputManager>
{
    [Header("Interact")]
    [SerializeField]private KeyCode _interactKey;





    [HideInInspector]public bool _interact;

    void Update()
    {
        _interact = Input.GetKeyDown(_interactKey);
    }
}
