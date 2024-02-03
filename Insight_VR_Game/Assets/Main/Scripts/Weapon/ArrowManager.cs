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

        if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            Debug.Log("적중");
            other.GetComponent<Monster>().OnHit();
            Destroy(gameObject);
        }

        if (other.CompareTag("Terrain"))
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * 0);
            Debug.Log("지형 오브젝트에 적중");
            Destroy(gameObject);
        }
    }
}
