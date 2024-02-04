using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTag : MonoBehaviour
{
    public PlayerType _tag;
    public List<bool> hadKeys = new List<bool>();
}

public enum PlayerType
{
    A,B,Both
}
