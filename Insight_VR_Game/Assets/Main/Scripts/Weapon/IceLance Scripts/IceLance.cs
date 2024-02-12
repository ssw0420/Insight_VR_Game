using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceLance : MonoBehaviour
{
    public ParticleSystem myParticleSystem;
    // Start is called before the first frame update
    void Start()
    {
        myParticleSystem.GetComponent<ParticleSystem>();
        gameObject.transform.SetParent(null);
    }

    void OnEnable()
    {
        myParticleSystem.Play();
    }

    void OnDisable()
    {
        myParticleSystem.Stop();
    }
}
