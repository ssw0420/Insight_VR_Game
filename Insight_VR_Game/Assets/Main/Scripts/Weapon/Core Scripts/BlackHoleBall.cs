using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BlackHoleBall : MonoBehaviour
{
    [SerializeField] private float cooldown_time = 8.0f;
    [SerializeField] private float using_time = 4.5f;

    [SerializeField] BlackHole BlackHoleScript;

    public GameObject BlackHolePrefab;

    // private AudioSource audioSource;
    public enum BlackHoleBallState
    {
        Idle,
        Fire,
        Cooldown
    }

    BlackHoleBallState _state;

    public Material[] skillMaterials;
    ParticleSystem[] skillEffect = new ParticleSystem[2];
    Renderer render;
    bool renderChange = false;

    private void Start()
    {
        // audioSource = GetComponent<AudioSource>();
        _state = BlackHoleBallState.Idle;
        skillEffect = GetComponentsInChildren<ParticleSystem>();
        render = GetComponent<Renderer>();
        skillEffect[0].Stop();
        skillEffect[1].Stop();
        Debug.Log("스킬 호출");
    }

    public void ShootBlackHole()
    {
        if(_state == BlackHoleBallState.Fire || _state == BlackHoleBallState.Cooldown)
        {
            return;
        }
        
        GameObject BlackHole = Instantiate(BlackHolePrefab, transform);
        BlackHoleScript = BlackHole.GetComponent<BlackHole>();
        using_time = 4.0f;
        _state = BlackHoleBallState.Fire;
        StartCoroutine(ChangeBall());
        Debug.Log("스킬 사용");
    }

    private void FixedUpdate() {
        switch(_state)
        {
            case BlackHoleBallState.Idle:
                UpdateIdle();
                break;
            case BlackHoleBallState.Fire:
                UpdateFire();
                break;
            case BlackHoleBallState.Cooldown:
                UpdateCooldown();
                break;
        }
    }

    void UpdateIdle()
    {
        cooldown_time = 8.0f;
        using_time = 4.5f;
    }

    void UpdateFire()
    {
        using_time -= Time.fixedDeltaTime;

        if(using_time <= 0.0f)
        {
            cooldown_time = 8.0f;
            _state = BlackHoleBallState.Cooldown;
            // StartCoroutine(ChangeBall());          
            Debug.Log("스킬 사용 종료");
        }
    }

    IEnumerator ChangeBall(){
        skillEffect[0].Play();
        yield return new WaitForSeconds(0.8f);
        render.material = skillMaterials[1];
    }

    void UpdateCooldown()
    {
        cooldown_time -= Time.fixedDeltaTime;
        if(cooldown_time <= 1.3f){
            if(renderChange)
                return;

            renderChange = true;
            StartCoroutine(Reload());
        }
        if(cooldown_time <= 0.0f)
        {
            _state = BlackHoleBallState.Idle;
            Debug.Log("스킬 쿨타임 종료");
        }
    }

    IEnumerator Reload()
    {
        skillEffect[1].Play();
        yield return new WaitForSeconds(1.3f);
        render.material = skillMaterials[0];
        yield return new WaitForSeconds(0.2f);
        renderChange = false;
    }
    // MeshRenderer render;
    
    // private void Awake()
    // {
    //     render = GetComponent<MeshRenderer>();
    // }

    // public void Throw()
    // {
    //     var interactable = GetComponent<XRGrabInteractable>();
    //     interactable.interactionManager.CancelInteractableSelection((IXRSelectInteractable)interactable);

    //     var rb = GetComponent<Rigidbody>();
    //     rb.AddRelativeForce(new Vector3(0f, 70f, 400f));
    // }

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
