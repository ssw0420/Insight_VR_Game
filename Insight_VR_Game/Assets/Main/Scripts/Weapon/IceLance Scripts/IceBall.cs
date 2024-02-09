using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : MonoBehaviour
{
    [SerializeField] private float cooldown_time = 8.0f;

    [SerializeField] private float using_time = 4.0f;
    private float shoot_time;
    private float reload_time;

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
    }

    public void ShootIceLance()
    {
        GameObject iceLance = Instantiate(iceLancePrefab, transform);
        iceLanceScript = iceLance.GetComponent<IceLance>();
        cooldown_time = 8.0f;
        _state = IceBallState.Fire;
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
        if(using_time <= 0.0f)
        {
            _state = IceBallState.Cooldown;
        }
    }

    void UpdateCooldown()
    {
        cooldown_time -= Time.fixedDeltaTime;
        if(cooldown_time <= 0.0f)
        {
            _state = IceBallState.Idle;
        }
    }
}
