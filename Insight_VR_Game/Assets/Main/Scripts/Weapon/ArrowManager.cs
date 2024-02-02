using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    ArrowManager arrowManager;
    // [SerializeField] GameObject crossbow;
    Rigidbody rigid;
    TrailRenderer trailRenderer;
    public float angle;
    public float power;
    public float speed = 0.5f;
    Vector3 v1;
    float Timedir;
    float shootTime;
    float gravity;

    // public void GetCrossbow(GameObject crossbow)
    // {
    //     this.crossbow = crossbow;
    // }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        trailRenderer = transform.Find("MoveTrack").GetComponent<TrailRenderer>();
    }

    private void Start()
    {
        transform.localPosition = new Vector3(0, 0.16572f, 0.20547f);
        trailRenderer.enabled = false;
        Timedir = Time.deltaTime;
        gravity = -(1.0f * Timedir * Timedir / 4.0f);
        // gravity = -9.8f; // 중력 감소
    }

    public void Fire()
    {
        rigid.isKinematic = false;
        trailRenderer.enabled = true;
        rigid.useGravity = true;
        gameObject.transform.SetParent(null);

        StartCoroutine("Move");
    }

    IEnumerator Move()
    {
        while (true)
        {
            yield return null;
            shootTime += Time.deltaTime;
            GetComponent<Rigidbody>().AddForce(transform.forward * speed);
            // if(shootTime < 0.15f)
            // {
            //     GetComponent<Rigidbody>().AddForce(transform.forward * speed);
            // }
            // else
            // {
            //     Timedir += Time.deltaTime / 2;
            //     rigid.isKinematic = false;
            //     trailRenderer.enabled = true;
            //     rigid.useGravity = true;
            //     v1.z = power = Mathf.Cos(angle * Mathf.PI / 180.0f) * Timedir;
            //     v1.y = power = Mathf.Cos(angle * Mathf.PI / 180.0f) * Timedir * gravity;
            //     transform.Translate(v1);

            //     transform.Rotate(new Vector3(Mathf.Cos(angle * Mathf.PI / 180.0f), 0, 0));
            // }
            // Timedir += Time.deltaTime;
            // v1.z = power = Mathf.Cos(angle * Mathf.PI / 180.0f) * Timedir;
            // v1.y = power = Mathf.Cos(angle * Mathf.PI / 180.0f) * Timedir * gravity;
            // transform.Translate(v1);

            // transform.Rotate(new Vector3(Mathf.Cos(angle * Mathf.PI / 180.0f), 0, 0));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 0);
        // if (other.CompareTag("Monster"))
        // {
        //     Debug.Log("적중");
        //     Destroy(gameObject);

        //     var MonsterScript = other.GetComponent<Monster>();
        //     MonsterScript.health -= 1;
        //     if (MonsterScript.health <= 0)
        //         MonsterScript.Die();
        //     else
        //         MonsterScript.Hit();
        // }

        if (other.CompareTag("Terrain"))
        {
            Debug.Log("지형 오브젝트에 적중");
            Destroy(gameObject);
        }
    }
}
