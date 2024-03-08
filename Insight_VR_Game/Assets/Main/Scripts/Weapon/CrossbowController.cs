using System.Collections;
using System.Collections.Generic;
// using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class CrossbowController : MonoBehaviour
{
    private Animator animator;
    private bool isShoot = false;

    public float shootDelay = 0.55f;
    private float animaitionShootDelay = 0.06f;

    public GameObject arrowPrefab;

    private AudioSource audioSource;

    private bool isFilling = false;

    [SerializeField] ArrowManager arrowManagerScript;
    [SerializeField] AudioSource drawAudio;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        drawAudio = GetComponentInChildren<AudioSource>();
    }
    private void Start()
    {
        StartCoroutine(Fill());
    }

    private IEnumerator Fill()
    {
        isFilling = true;

        DrawingBow();

        isShoot = false;

        var wfs = new WaitForSeconds(shootDelay);
        yield return wfs;

        animator.SetBool("isEmpty", false);
        yield return new WaitForSeconds(animaitionShootDelay);
        LoadArrow();

        isFilling = false;
        Debug.Log("재장전 완료");
        
    }

    public void Shoot()
    {
        if(!isShoot || isFilling)
            return;

        animator.SetTrigger("ShootTrigger");
        arrowManagerScript.Fire();
        audioSource.Play();
        Debug.Log("발사");
        if(!isFilling)
        {
            StartCoroutine(Fill());
        }
        Debug.Log("재장전 시작");
    }

    void LoadArrow()
    {

        GameObject arrow = Instantiate(arrowPrefab, transform);
        arrowManagerScript = arrow.GetComponent<ArrowManager>();

        isShoot = true;
    }

    void DrawingBow()
    {
        Debug.Log("장전");
        animator.SetBool("isEmpty", true);
        drawAudio.Play();
    }
}
