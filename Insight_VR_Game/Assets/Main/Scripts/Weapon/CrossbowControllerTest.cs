using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowControllerTest : MonoBehaviour
{

    private Animator animator;
    private bool isShoot = false;

    public float shootDelay = 0.8f;

    public GameObject arrowPrefab;

    private AudioSource audioSource;

    private bool isFilling = false;
    // public Transform shootPoint;

    public enum CrossbowState
    {
        Empty,
        Drawing,
        Holding,
        Shooting,
        Die
    }

    CrossbowState _state;

    [SerializeField] ArrowManager arrowManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        Debug.Log("애니메이션, 오디오 호출");
        _state = CrossbowState.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        switch(_state)
        {
            case CrossbowState.Empty:
                UpdateEmpty();
                break;
            case CrossbowState.Drawing:
                UpdateDrawing();
                break;
            case CrossbowState.Holding:
                UpdateHolding();
                break;
            case CrossbowState.Shooting:
                UpdateShooting();
                break;
            case CrossbowState.Die:
                UpdateDie();
                break;
        }
    }

    void UpdateEmpty()
    {
        StartCoroutine(Fill());
    }

    void UpdateDrawing()
    {
        GameObject.Find("Bow Line").GetComponent<AudioSource>().Play();
    }

    void UpdateHolding()
    {

    }

    void UpdateShooting()
    {

    }
    void UpdateDie()
    {

    }

    void LoadArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, transform);
        arrowManagerScript = arrow.GetComponent<ArrowManager>();
        // arrowManagerScript.GetCrossbow(gameObject);
        isShoot = true;
        Debug.Log("장전 완료");
    }

        private IEnumerator Fill()
    {
        isFilling = true;
        Debug.Log("장전");
        animator.SetBool("isEmpty", true);
        DrawingBow();
        isShoot = false;
        var wfs = new WaitForSeconds(shootDelay);
        yield return wfs;
        // yield return null;
        //animator.SetBool("isEmpty", true);
        animator.SetBool("isEmpty", false);
        LoadArrow();
        isFilling = false;
        Debug.Log("재장전 완료");
        //animator.SetBool("isEmpty", false);
        
    }

        void DrawingBow()
    {
        GameObject.Find("Bow Line").GetComponent<AudioSource>().Play();
    }


}
