using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceLance : MonoBehaviour
{
    private AudioSource audioSource;
    private float iceLance_time = 0.0f;
    private int icelance_dmg = 3;
    //public ParticleSystem myParticleSystem;
    // Start is called before the first frame update

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        //myParticleSystem.GetComponent<ParticleSystem>();
        gameObject.transform.SetParent(null);
        audioSource.Play();
    }

    private void FixedUpdate() {
        iceLance_time += Time.fixedDeltaTime;
        if(iceLance_time >= 3.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.layer == LayerMask.NameToLayer("Monster"))
        {
            other.GetComponent<Monster>().OnHit(icelance_dmg, "Ice");
            Destroy(gameObject, 0.3f);
        }
        else if (other.layer == LayerMask.NameToLayer("Hit Monster"))
        {
            other.GetComponent<Monster>().OnHit(icelance_dmg, "Ice");
            Destroy(gameObject);
        }    
            
    }

}
