using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : MonoBehaviour
{
    [SerializeField] private float cooldown_time = 8.0f;

    [SerializeField] private float using_time = 4.0f;
    private float shoot_time;
    private float reload_time;
    private float iceLance_time;

    [SerializeField] IceLance iceLanceScript;

    public GameObject iceLancePrefab;

    // private AudioSource audioSource;
    public enum IceBallState
    {
        Idle,
        Fire,
        Cooldown,
        Reloading
    }

    IceBallState _state;

    public Material[] skillMaterials;
    ParticleSystem[] skillEffect = new ParticleSystem[2];
    Renderer render;
    // public Material[] materials = new Material[2];

    bool renderChange = false;

    private void Start()
    {
        // audioSource = GetComponent<AudioSource>();
        _state = IceBallState.Idle;
        skillEffect = GetComponentsInChildren<ParticleSystem>();
        // gameObject.GetComponent<MeshRenderer>().material = materials[0];
        render = GetComponent<Renderer>();
        render.material = skillMaterials[0];
        skillEffect[0].Stop();
        skillEffect[1].Stop();
        Debug.Log("스킬 호출");
    }

    public void ShootIceLance()
    {
        if(_state == IceBallState.Fire || _state == IceBallState.Cooldown || _state == IceBallState.Reloading)
        {
            return;
        }
        using_time = 4.0f;
        _state = IceBallState.Fire;
        Debug.Log("스킬 사용");
    }

    private void FixedUpdate() {
        switch(_state)
        {
            case IceBallState.Idle:
                UpdateIdle();
                break;
            case IceBallState.Fire:
                UpdateFire();
                break;
            case IceBallState.Cooldown:
                UpdateCooldown();
                break;
            case IceBallState.Reloading:
                break;
        }
    }

    void UpdateIdle()
    {
        cooldown_time = 8.0f;
        using_time = 4.0f;
    }

    void UpdateFire()
    {
        using_time -= Time.fixedDeltaTime;
        iceLance_time += Time.fixedDeltaTime;

        if(iceLance_time >= 0.45f)
        {
            iceLance_time = 0.0f;
            GameObject iceLance = Instantiate(iceLancePrefab, transform);
            iceLanceScript = iceLance.GetComponent<IceLance>();
        }

        if(using_time <= 0.0f)
        {
            cooldown_time = 8.0f;
            _state = IceBallState.Cooldown;
            StartCoroutine(ChangeBall());          
            Debug.Log("스킬 사용 종료");
        }
    }

    IEnumerator ChangeBall(){
        skillEffect[0].Play();
        yield return new WaitForSeconds(0.8f);
        render.material = skillMaterials[1];
        // gameObject.GetComponent<MeshRenderer>().material = materials[0];
    }

    void UpdateCooldown()
    {
        cooldown_time -= Time.fixedDeltaTime;
        // if(cooldown_time <= 1.8f){
        //     if(renderChange)
        //         return;

        //     // renderChange = true;
        //     StartCoroutine(Reload());
        // }
        if(cooldown_time <= 0.0f)
        {
            StartCoroutine(Reload());
            _state = IceBallState.Reloading;
            Debug.Log("스킬 쿨타임 종료");
        }
    }

    // void UpdateReloading()
    // {
    //     _state = IceBallState.Cooldown;
    // }

    IEnumerator Reload()
    {
        Debug.Log("스킬 재장전 중");
        skillEffect[1].Play();
        yield return new WaitForSeconds(1.7f);
        // skillEffect[1].Stop();
        render.material = skillMaterials[0];
        _state = IceBallState.Idle;
        // gameObject.GetComponent<MeshRenderer>().material = materials[1];
        //yield return new WaitForSeconds(0.2f);

    }
}
