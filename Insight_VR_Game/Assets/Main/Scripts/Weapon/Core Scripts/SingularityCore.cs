﻿using System.Collections;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class SingularityCore : MonoBehaviour
{
    //This script is responsible for what happens when the pullable objects reach the core
    //by default, the game objects are simply turned off
    //as this is much more performant than destroying the objects

    private AudioSource audioSource;
    void OnTriggerStay (Collider other) {
        if(other.GetComponent<SingularityPullable>()){
            //other.GetComponent<Monster>().OnHit();
        }
    }

    void Awake(){
        if(GetComponent<SphereCollider>()){
            GetComponent<SphereCollider>().isTrigger = true;
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnEnable()
    {
        audioSource.Play();
    }

    void OnDestroy() {
        audioSource.Stop();
    }
}
