using System.Collections;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class SingularityCore : MonoBehaviour
{
/*    void OnTriggerStay (Collider other) {
        if(other.GetComponent<SingularityPullable>()){
            //other.GetComponent<Monster>().OnHit();
        }
    }*/

    void Awake(){
        if(GetComponent<SphereCollider>()){
            GetComponent<SphereCollider>().isTrigger = true;
        }
    }
}
