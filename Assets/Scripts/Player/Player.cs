using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private PlayerItemHandler _playerItemHandler;
    [SerializeField]private PlayerTag _playerTag;

    public void Init()
    {
        _playerItemHandler.Init();
        // _playerTag.Init();
    }
}
