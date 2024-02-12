using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceLance : MonoBehaviour
{
<<<<<<< HEAD
<<<<<<< HEAD
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
=======
    public ParticleSystem myParticleSystem;
    // Start is called before the first frame update
    void Start()
    {
=======
    public ParticleSystem myParticleSystem;
    // Start is called before the first frame update
    void Start()
    {
>>>>>>> parent of 58c5b48 (.)
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
<<<<<<< HEAD
>>>>>>> parent of 58c5b48 (.)
=======
>>>>>>> parent of 58c5b48 (.)
    }
}
