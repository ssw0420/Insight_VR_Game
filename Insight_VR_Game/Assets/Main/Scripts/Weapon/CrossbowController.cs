using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class CrossbowController : MonoBehaviour
{
    private Animator animator;
    private bool isShoot = false;

    public float shootDelay = 1f;

    public GameObject arrowPrefab;
    // public Transform shootPoint;

    [SerializeField] ArrowManager arrowManagerScript;

    // public UnityEvent<Vector3> OnShootSuccess;
    // public UnityEvent OnShootFail;

    private void Awake()
    {
        // GameObject arrow = Instantiate(arrowPrefab, transform);
        // arrowManagerScript = arrow.GetComponent<ArrowManager>();
        // arrowManagerScript.GetCrossbow(gameObject);
        LoadArrow();
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        // LoadArrow();
    }

    private IEnumerator Fill()
    {
        isShoot = false;
        var wfs = new WaitForSeconds(shootDelay);
        yield return wfs;
        
        LoadArrow();
    }

    public void Shoot()
    {
        if(!isShoot)
            return;

        // StopAllCoroutines();
        arrowManagerScript.Fire();
        StartCoroutine(Fill());
    }

    void LoadArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, transform);
        arrowManagerScript = arrow.GetComponent<ArrowManager>();
        // arrowManagerScript.GetCrossbow(gameObject);
        isShoot = true;
    }
}
