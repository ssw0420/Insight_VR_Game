using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameManager instance;
    public Spawner spawner;

    List<Monster> monster;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        monster = new List<Monster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            monster[0].OnHit();
            if (monster[0].health <= 0)
                monster.Remove(monster[0]);
        }
    }

    public void InputList(Monster monster)
    {
        this.monster.Add(monster);
    }
}
