using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    ArrowManager arrowManager;
    // [SerializeField] GameObject crossbow;
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
    //private int dmgstate;
    // public bool dmgstate = false;



    bool isShoot;

    // public void GetCrossbow(GameObject crossbow)
    // {
    //     this.crossbow = crossbow;
    // }

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
        
        // StartCoroutine("Move");
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

    // IEnumerator Move()
    // {
    //     while (true)
    //     {
    //         yield return new WaitForFixedUpdate();
    //         shootTime += Time.deltaTime;
    //         GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    //         // if(shootTime < 0.15f)
    //         // {
    //         //     GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    //         // }
    //         // else
    //         // {
    //         //     Timedir += Time.deltaTime / 2;
    //         //     rigid.isKinematic = false;
    //         //     trailRenderer.enabled = true;
    //         //     rigid.useGravity = true;
    //         //     v1.z = power = Mathf.Cos(angle * Mathf.PI / 180.0f) * Timedir;
    //         //     v1.y = power = Mathf.Cos(angle * Mathf.PI / 180.0f) * Timedir * gravity;
    //         //     transform.Translate(v1);

    //         //     transform.Rotate(new Vector3(Mathf.Cos(angle * Mathf.PI / 180.0f), 0, 0));
    //         // }
    //         // Timedir += Time.deltaTime;
    //         // v1.z = power = Mathf.Cos(angle * Mathf.PI / 180.0f) * Timedir;
    //         // v1.y = power = Mathf.Cos(angle * Mathf.PI / 180.0f) * Timedir * gravity;
    //         // transform.Translate(v1);

    //         // transform.Rotate(new Vector3(Mathf.Cos(angle * Mathf.PI / 180.0f), 0, 0));
    //         if(shootTime >= 3.0f)
    //         {
    //             Destroy(gameObject);
    //         }
    //     }
    // }

    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster") && !PlayerController.instance.DmgState)
        {
            Debug.Log("1 적중");
            other.GetComponent<Monster>().OnHit(1);
            Destroy(gameObject);
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Monster") && PlayerController.instance.DmgState)
        {
            Debug.Log("2 적중");
            other.GetComponent<Monster>().OnHit(2);
            Destroy(gameObject);
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("HealthMonster") && !PlayerController.instance.DmgState)
        {
            other.GetComponent<HealthMonster>().OnHit(1);
            Destroy(gameObject);
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("HealthMonster") && PlayerController.instance.DmgState)
        {
            other.GetComponent<HealthMonster>().OnHit(2);
            Destroy(gameObject);
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("Boss Monster") && !PlayerController.instance.DmgState)
        {
            other.GetComponentInParent<Boss>().OnHit(1);
            Destroy(gameObject);
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Boss Monster") && PlayerController.instance.DmgState)
        {
            other.GetComponentInParent<Boss>().OnHit(2);
            Destroy(gameObject);
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Boss Eye") && !PlayerController.instance.DmgState)
        {
            other.GetComponentInParent<Boss>().OnHit(5);
            Destroy(gameObject);
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Boss Eye") && PlayerController.instance.DmgState)
        {
            other.GetComponentInParent<Boss>().OnHit(10);
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
            Debug.Log("지형 오브젝트에 적중");
            Destroy(gameObject, 1.0f);
        }



        


        if (other.CompareTag("BlackHole"))
        {
            Debug.Log("블랙홀");
            ChoiceCard.instance.ChoiceBlackHole();
            MonsterManager.Instance.ReadSpawnFile();
            //Destroy(gameObject);
        }
        else if (other.CompareTag("Ice"))
        {
            Debug.Log("아이스");
            ChoiceCard.instance.ChoiceIceBall();
            MonsterManager.Instance.ReadSpawnFile();
            //Destroy(gameObject);
        }
        else if (other.CompareTag("Upgrade_1"))
        {
            Debug.Log("공격력 2배");
            Debug.Log("PlayerController.instance.dmgstate = " + PlayerController.instance.DmgState);
            ChoiceCard.instance.ChoiceUpgrade_1();
            MonsterManager.Instance.ReadSpawnFile();
            //Destroy(gameObject);
        }
        else if (other.CompareTag("Upgrade_2"))
        {
            Debug.Log("최대 체력 회복");
            ChoiceCard.instance.ChoiceUpgrade_2();
            MonsterManager.Instance.ReadSpawnFile();
            //Destroy(gameObject);
        }

    }
}
