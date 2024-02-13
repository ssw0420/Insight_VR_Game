using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    Camera player;
    bool isMove = false;

    private void Start()
    {
        player = Camera.main;
    }

    private void FixedUpdate()
    {
        if (isMove)
            MoveToPlayer();
    }

    void MoveToPlayer()
    {
        transform.position = Vector3.Lerp(gameObject.transform.position, player.transform.position, 0.05f);

        if(Vector3.Distance(transform.position, player.transform.position) <= 0.1f)
        {
            PlayerStats.Instance.Heal(1);
            Destroy(gameObject);
        }
    }

    public void PotionMove()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(2f);

        isMove = true;
    }
}
