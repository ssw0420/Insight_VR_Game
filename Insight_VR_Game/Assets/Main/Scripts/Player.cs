using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public XRController leftController;
    public XRController rightController;

    AudioSource hitAudio;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        hitAudio = GetComponent<AudioSource>();
    }

    public void PlayerHit()
    {
        hitAudio.Play();
        leftController.SendHapticImpulse(0.3f, 0.3f);
        rightController.SendHapticImpulse(0.3f, 0.3f);
    }
}
