using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]private PlayerType correctTpye;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player") && (other.GetComponent<PlayerTag>()._tag == correctTpye || correctTpye == PlayerType.Both))
        {
            if (other.GetComponent<PlayerTag>()._tag == PlayerType.A && GetComponent<CheckPass>().IsQualified(0))
            {
                Debug.Log("CheckPoint");
                GameFlowManager.instance.PassLevel();
                GetComponent<CheckPass>().EnterDoor(0);
            }
            else if (other.GetComponent<PlayerTag>()._tag == PlayerType.B && GetComponent<CheckPass>().IsQualified(1))
            {
                Debug.Log("CheckPoint");
                GameFlowManager.instance.PassLevel();  
                GetComponent<CheckPass>().EnterDoor(1);
            }
        }
    }
}
