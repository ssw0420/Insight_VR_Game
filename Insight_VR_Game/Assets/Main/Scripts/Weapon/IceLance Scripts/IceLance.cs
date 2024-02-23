using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceLance : MonoBehaviour
{
    private AudioSource audioSource;
    private float iceLance_time = 0.0f;
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

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
    //     {
    //         other.GetComponent<Monster>().OnHit(1);
    //     }
    //     else if(other.gameObject.layer == LayerMask.NameToLayer("HealthMonster"))
    //     {
    //         other.GetComponent<HealthMonster>().OnHit(1);
    //     }
    // }
    private void OnParticleCollision(GameObject other)
    {
        if(other.layer == LayerMask.NameToLayer("Monster"))
        {
            Debug.Log("몬스터 스킬 히트 판정");
            other.GetComponent<Monster>().OnHit(1);
        }
            
    }

    //private void OnParticleTrigger()
    //{
    //    Debug.Log("IceLance Trigger");
    //}

}
