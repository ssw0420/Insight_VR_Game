using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceLance : MonoBehaviour
{
    private float iceLance_start_time = 0.0f;
    //public ParticleSystem myParticleSystem;
    // Start is called before the first frame update
    void Start()
    {
        //myParticleSystem.GetComponent<ParticleSystem>();
        gameObject.transform.SetParent(null);
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
        iceLance_start_time += Time.fixedDeltaTime;
        if(iceLance_start_time >= 3.0f)
        {
            Destroy(gameObject);
        }
    }
}
