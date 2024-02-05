using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Monster
{
    int deathCount = 5;

    public override void OnHit(GameObject hitPoint)
    {
        //base.OnHit();
        if (anim.GetBool("isHit"))
            return;

        deathCount++;
        if (deathCount >= 6)
            Die();

        anim.SetBool("isHit", true);
        Destroy(hitPoint);
        
        StartCoroutine("HitOut");
    }

    IEnumerator HitOut()
    {
        float saveSpeed = agent.speed;
        agent.speed = 0;
        Material saveMeterial = render.materials[0];
        render.material = hitMaterial;

        yield return new WaitForSeconds(1f);

        render.material = saveMeterial;
        anim.SetBool("isHit", false);
        agent.speed = saveSpeed;
    }
}
