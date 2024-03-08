using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    ArrowManager arrowManager;
    Rigidbody rigid;
    TrailRenderer trailRenderer;
    BoxCollider boxCollider;
    public float angle;
    public float power;
    public float speed = 1.1f;
    Vector3 v1;
    float Timedir;
    float shootTime;
    float gravity;
    bool isShoot;

    public static ArrowManager instance;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        trailRenderer = transform.Find("MoveTrack").GetComponent<TrailRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        transform.localPosition = new Vector3(0, 0.16572f, 0.20547f);
        trailRenderer.enabled = false;
        boxCollider.enabled = false;
        Timedir = Time.deltaTime;
        gravity = -(1.0f * Timedir * Timedir / 4.0f);
    }

    public void Fire()
    {
        isShoot = true;
        rigid.isKinematic = false;
        trailRenderer.enabled = true;
        boxCollider.enabled = true;
        rigid.useGravity = false;
        gameObject.transform.SetParent(null);
    }

    private void FixedUpdate()
    {
        if(isShoot == true)
        {
            ApplyForce();
        }
    }

    private void ApplyForce()
    {
        shootTime += Time.fixedDeltaTime;
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        if(shootTime >= 3.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.layer == LayerMask.NameToLayer("Monster") || other.gameObject.layer == LayerMask.NameToLayer("Hit Monster")) && !PlayerController.instance.DmgState)
        {
            other.GetComponent<Monster>().OnHit(1, "Arrow");
            Destroy(gameObject);
        }
        else if((other.gameObject.layer == LayerMask.NameToLayer("Monster") || other.gameObject.layer == LayerMask.NameToLayer("Hit Monster")) && PlayerController.instance.DmgState)
        {
            other.GetComponent<Monster>().OnHit(2, "Arrow");
            Destroy(gameObject);
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("HealthMonster") && !PlayerController.instance.DmgState)
        {
            other.GetComponent<HealthMonster>().OnHit(1, "Arrow");
            Destroy(gameObject);
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("HealthMonster") && PlayerController.instance.DmgState)
        {
            other.GetComponent<HealthMonster>().OnHit(2, "Arrow");
            Destroy(gameObject);
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("Boss Monster") && !PlayerController.instance.DmgState)
        {
            other.GetComponentInParent<Boss>().OnHit(1, "Arrow");
            Destroy(gameObject);
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Boss Monster") && PlayerController.instance.DmgState)
        {
            other.GetComponentInParent<Boss>().OnHit(2, "Arrow");
            Destroy(gameObject);
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Boss Eye") && !PlayerController.instance.DmgState)
        {
            other.GetComponentInParent<Boss>().OnHit(5, "Arrow");
            Destroy(gameObject);
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Boss Eye") && PlayerController.instance.DmgState)
        {
            other.GetComponentInParent<Boss>().OnHit(10, "Arrow");
            Destroy(gameObject);
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Boss Leg") && !PlayerController.instance.DmgState)
        {
            other.GetComponentInParent<Boss>().OnCriticalHit(3);
            Destroy(gameObject);
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Boss Leg") && PlayerController.instance.DmgState)
        {
            other.GetComponentInParent<Boss>().OnCriticalHit(6);
            Destroy(gameObject);
        }

        if (other.CompareTag("Terrain"))
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * 0);
            Destroy(gameObject, 1.0f);
        }
    }
}
