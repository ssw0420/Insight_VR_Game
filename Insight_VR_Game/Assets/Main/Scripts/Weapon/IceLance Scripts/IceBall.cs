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

    private AudioSource audioSource;
    public enum IceBallState
    {
        Idle,
        Fire,
        Cooldown
    }

    IceBallState _state;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _state = IceBallState.Idle;
        Debug.Log("스킬 호출");
    }

    public void ShootIceLance()
    {
        if(_state == IceBallState.Fire || _state == IceBallState.Cooldown)
        {
            return;
        }
        //GameObject iceLance = Instantiate(iceLancePrefab, transform);
        //iceLanceScript = iceLance.GetComponent<IceLance>();
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
            Debug.Log("스킬 사용 종료");
        }
    }

    void UpdateCooldown()
    {
        cooldown_time -= Time.fixedDeltaTime;
        if(cooldown_time <= 0.0f)
        {
            _state = IceBallState.Idle;
            Debug.Log("스킬 쿨타임 종료");
        }
    }
}
