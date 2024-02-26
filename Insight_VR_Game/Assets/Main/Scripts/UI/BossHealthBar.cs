
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    protected static BossHealthBar instance;

    public static BossHealthBar Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    [SerializeField]SpriteRenderer hpBar;
    [SerializeField]SpriteRenderer hpGhostBar;

    Camera player;

    private void Start()
    {
        hpBar = GetComponentsInChildren<SpriteRenderer>()[1];
        hpGhostBar = GetComponentsInChildren<SpriteRenderer>()[2];
        player = Camera.main;
    }

    private void FixedUpdate()
    {
        Vector3 lookDir = (player.transform.position - transform.position).normalized;

        Quaternion from = transform.rotation;
        Quaternion to = Quaternion.LookRotation(lookDir);

        transform.rotation = Quaternion.Lerp(from, to, Time.fixedDeltaTime * 9f);
    }

    public void HealthUIUpdate(int maxHealth, int health)
    {
        StartCoroutine(BossHealthBarUpdate(maxHealth, health));
    }

    IEnumerator BossHealthBarUpdate(int maxHealth, int health)
    {
        float healthPercent = (float)health / maxHealth;
        hpBar.size = new Vector2(healthPercent, hpBar.size.y);

        yield return new WaitForSeconds(1f);

        float ghostBar = hpGhostBar.size.x;
        float delta = 0f;
        float duration = 1f;
        while(delta <= duration)
        {
            float t = delta / duration;

            ghostBar = Mathf.Lerp(ghostBar, healthPercent, t);
            hpGhostBar.size = new Vector2(ghostBar, hpGhostBar.size.y);

            delta += Time.deltaTime;
        }
    }
}
