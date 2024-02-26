using MagicPigGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    Camera player;
    Vector3 eatPos;
    bool isMove = false;

    private void Start()
    {
        player = Camera.main;
        eatPos = player.transform.position + Vector3.down;
    }

    private void FixedUpdate()
    {
        if (isMove)
            MoveToPlayer();
    }

    void MoveToPlayer()
    {
        transform.position = Vector3.Lerp(gameObject.transform.position, eatPos, 0.05f);

        if(Vector3.Distance(transform.position, eatPos) <= 0.1f)
        {
            if(PlayerController.instance.HealthState == false)
                ProgressBarInspectorTest.instance.progress += 0.1f;
            else if (PlayerController.instance.HealthState == true)
                ProgressBarInspectorTest.instance.progress += 0.05f;
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
