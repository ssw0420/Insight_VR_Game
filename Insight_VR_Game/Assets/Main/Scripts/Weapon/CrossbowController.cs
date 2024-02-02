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

    public float shootDelay = 0.8f;

    public GameObject arrowPrefab;

    private AudioSource audioSource;

    //Transform pos;
    private bool isFilling = false;
    // public Transform shootPoint;

    [SerializeField] ArrowManager arrowManagerScript;

    // public UnityEvent<Vector3> OnShootSuccess;
    // public UnityEvent OnShootFail;

    private void Awake()
    {
        // GameObject arrow = Instantiate(arrowPrefab, transform);
        // arrowManagerScript = arrow.GetComponent<ArrowManager>();
        // arrowManagerScript.GetCrossbow(gameObject);
        // LoadArrow();
        Debug.Log("시작");
        // animator.SetBool("isEmpty", true);
        // LoadArrow();
        //pos = GetComponent<Transform>();
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        Debug.Log("애니메이션, 오디오 호출");
        StartCoroutine(Fill());
        // LoadArrow();
    }

    private IEnumerator Fill()
    {
        isFilling = true;

        DrawingBow();

        isShoot = false;

        var wfs = new WaitForSeconds(shootDelay);
        yield return wfs;

        animator.SetBool("isEmpty", false);
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
        GameObject.Find("Bow Line").GetComponent<AudioSource>().Play();
    }
}
