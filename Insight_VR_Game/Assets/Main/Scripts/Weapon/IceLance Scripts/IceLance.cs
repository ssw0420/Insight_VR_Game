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

    // void OnEnable()
    // {
    //     myParticleSystem.Play();
    // }

    // void OnDisable()
    // {
    //     myParticleSystem.Stop();
    // }
    private void FixedUpdate() {
        iceLance_time += Time.fixedDeltaTime;
        if(iceLance_time >= 3.0f)
        {
            Destroy(gameObject);
        }
    }
}
