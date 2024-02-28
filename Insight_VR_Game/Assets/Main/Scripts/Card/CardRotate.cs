using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRotate : MonoBehaviour
{
    void Update()
    {
       if(ChoiceCard.instance.CardChoice == true)
        {
            Debug.Log("È¸Àü");
            
            transform.Rotate(new Vector3(0, 50, 0) * Time.deltaTime);
        }
    }
}
