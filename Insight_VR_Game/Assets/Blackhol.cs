using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Blackhol : MonoBehaviour
{
    MeshRenderer render;

    private void Awake()
    {
        render = GetComponent<MeshRenderer>();
    }

    public void Throw()
    {
        var interactable = GetComponent<XRGrabInteractable>();
        interactable.interactionManager.CancelInteractableSelection((IXRSelectInteractable)interactable);

        var rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(new Vector3(0f, 70f, 400f));
    }

    private void OnCollisionEnter(Collision collision)
    {   
        StartSkill();
    }

    void StartSkill()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        GetComponent<Rigidbody>().isKinematic = true;
        render.enabled = false;
    }
}
