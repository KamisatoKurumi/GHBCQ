using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateform_Destroy : MonoBehaviour
{
    private Animator anim;
    [SerializeField]private ParticleSystem particle;
    [SerializeField]private float timeBeforeDestroy = 1f;
    
    void Start()
    {
        particle.Stop();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(DestroyPlateform());
        }
    }

    private IEnumerator DestroyPlateform()
    {
        anim.SetTrigger("CollapseSoon");
        yield return new WaitForSeconds(timeBeforeDestroy);
        anim.SetTrigger("Collapse");
    }

    public void OnBoxColliderDisable()
    {
        AudioManager.PlayAudio(AudioName.PlateformBreak);
        particle.Play();
    }

    public void OnAnimationEnd()
    {
        Destroy(this.gameObject);
    }
}
