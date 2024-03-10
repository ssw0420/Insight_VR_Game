using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;
[RequireComponent(typeof(SphereCollider))]
public class SingularityCore : MonoBehaviour
{
    private SphereCollider sphereCollider;
    /*    void OnTriggerStay (Collider other) {
            if(other.GetComponent<SingularityPullable>()){
                //other.GetComponent<Monster>().OnHit();
            }
        }*/

    //GameObject RoundObject;
    float time = 0;
    bool isBomb = false;
    //void Awake(){
    //    RoundObject = GetComponentInChildren<GameObject>();
    //}

    private void FixedUpdate()
    {
        time += Time.fixedDeltaTime;

        if(time >= 1f && time <= 1.5f)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            isBomb = true;
            Debug.Log("1.0");
        }
        else if(time > 1.5f)
        {
            Debug.Log("1.5");
            time = 0;
            isBomb = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Monster") || other.gameObject.layer == LayerMask.NameToLayer("Health Monster"))
        {
            Debug.Log("onBlackHoleHitMonster");
            if (!isBomb)
                return;

            other.GetComponent<Monster>().OnHit(1, "BlackHole");
            Debug.Log("ontrigger");
        }
    }
}
