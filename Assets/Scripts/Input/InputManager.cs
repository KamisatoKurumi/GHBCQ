using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoSingleton<InputManager>
{
    [Header("Interact")]
    [SerializeField]private KeyCode _interactKey = KeyCode.E;
    [SerializeField]private KeyCode _resetGameKey = KeyCode.R;
    [SerializeField]private KeyCode _stopGameKey = KeyCode.Escape;
    
    [SerializeField]private KeyCode _interactLever = KeyCode.E;




    [HideInInspector]public bool _interact;
    [HideInInspector]public bool _resetGame;
    [HideInInspector]public bool _stopGame;
    
    [HideInInspector]public bool _interactL;

    void Update()
    {
        _interact = Input.GetKeyDown(_interactKey);
        _resetGame = Input.GetKeyDown(_resetGameKey);
        _stopGame = Input.GetKeyDown(_stopGameKey);
        _interactL = Input.GetKeyDown(_interactLever);
    }
}
