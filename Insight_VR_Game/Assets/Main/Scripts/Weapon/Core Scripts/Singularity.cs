using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class Singularity : MonoBehaviour
{
    //This is the main script which pulls the objects nearby
    [SerializeField] public float GRAVITY_PULL = 100f;
    public static float m_GravityRadius = 1f;
    private AudioSource audioSource;

    void Awake() {
        m_GravityRadius = GetComponent<SphereCollider>().radius;
        audioSource = GetComponent<AudioSource>();

        if (GetComponent<SphereCollider>()){
            GetComponent<SphereCollider>().isTrigger = true;
        }
    }
    
    void OnTriggerStay (Collider other) {
        if(other.GetComponent<SingularityPullable>()) {
            Debug.Log("몬스터 블랙홀 맞음");
            other.GetComponent<Monster>().HitBlackHole(transform.position);

            //float gravityIntensity = Vector3.Distance(transform.position, other.transform.position) / m_GravityRadius;
            //other.attachedRigidbody.AddForce((transform.position - other.transform.position) * gravityIntensity * other.attachedRigidbody.mass * GRAVITY_PULL * Time.smoothDeltaTime);
        }
    }

    void OnEnable()
    {
        audioSource.Play();
    }

    void OnDestroy()
    {
        audioSource.Stop();
        MonsterManager.Instance.ReDestination();
    }
}
