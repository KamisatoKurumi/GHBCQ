using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key2 : MonoBehaviour
{
    [SerializeField] private KeyTpye keyTpye;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerTag>()._tag == PlayerType.A && other.GetComponent<PlayerTag>().hadKeys[(int)keyTpye]==false)
        {
            other.GetComponent<PlayerTag>().hadKeys[(int)keyTpye] = true;
            //更新UI
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Player") && other.GetComponent<PlayerTag>()._tag == PlayerType.B && other.GetComponent<PlayerTag>().hadKeys[(int)keyTpye] == false)
        {
            other.GetComponent<PlayerTag>().hadKeys[(int)keyTpye] = true;
            //更新UI
            Destroy(this.gameObject);
        }
    }
}
