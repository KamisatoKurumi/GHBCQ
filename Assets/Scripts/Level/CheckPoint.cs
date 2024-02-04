using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyTpye
{
    Zero,One,Two,Three
}

public class CheckPoint : MonoBehaviour
{
    [SerializeField]private PlayerType correctTpye;
    [SerializeField]private KeyTpye keyTpye;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player") && (other.GetComponent<PlayerTag>()._tag == correctTpye || correctTpye == PlayerType.Both))
        {
            if (keyTpye == KeyTpye.Zero)
            {
                Debug.Log("CheckPoint");
                GameFlowManager.instance.PassLevel();
            }
            if (keyTpye == KeyTpye.One)
            {
                if (other.GetComponent<PlayerTag>().hadKeys[(int)KeyTpye.One]==true)
                {
                    Debug.Log("CheckPoint");
                    GameFlowManager.instance.PassLevel();
                }
            }
            if (keyTpye == KeyTpye.Two)
            {
                if (other.GetComponent<PlayerTag>().hadKeys[(int)KeyTpye.One] == true && other.GetComponent<PlayerTag>().hadKeys[(int)KeyTpye.Two] == true)
                {
                    Debug.Log("CheckPoint");
                    GameFlowManager.instance.PassLevel();
                }
            }
            if (keyTpye == KeyTpye.Three)
            {
                if (other.GetComponent<PlayerTag>().hadKeys[(int)KeyTpye.One] == true && other.GetComponent<PlayerTag>().hadKeys[(int)KeyTpye.Two] == true && other.GetComponent<PlayerTag>().hadKeys[(int)KeyTpye.Three] == true)
                {
                    Debug.Log("CheckPoint");
                    GameFlowManager.instance.PassLevel();
                }
            }
        }
    }
}
