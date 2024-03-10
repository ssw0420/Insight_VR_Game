using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BlackHole : MonoBehaviour
{
    private bool onGround = false;
    private float blackHole_time = 4.5f;

    private SphereCollider sphereCollider;
    public void Start()
    {
        var rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        gameObject.transform.SetParent(null);
        rb.AddRelativeForce(new Vector3(0f, 70f, 400f));
    }

    private void FixedUpdate()
    {
        if(onGround)
        {
            blackHole_time -= Time.fixedDeltaTime;
                    
            if(blackHole_time <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            onGround = true;
            StartSkill();
        }
    }

    private void StartSkill()
    {
         transform.GetChild(0).gameObject.SetActive(true);
         transform.GetChild(2).gameObject.SetActive(true);
         GetComponent<Rigidbody>().isKinematic = true;
    }

    // private void OnCollisionEnter(Collision collision)
    // {   
    //     StartSkill();
    // }

    // void StartSkill()
    // {
    //     transform.GetChild(0).gameObject.SetActive(true);
    //     GetComponent<Rigidbody>().isKinematic = true;
    //     render.enabled = false;
    // }
}
