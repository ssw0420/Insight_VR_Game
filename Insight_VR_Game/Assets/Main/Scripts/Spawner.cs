using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    List<GameObject> monsterPrefabs;
    bool isSpawn = false;

    private void Awake()
    {
        monsterPrefabs = new List<GameObject>();
        monsterPrefabs.Add(Resources.Load("Prefabs/SlimePBR") as GameObject);
        monsterPrefabs.Add(Resources.Load("Prefabs/TurtleShellPBR") as GameObject);
        monsterPrefabs.Add(Resources.Load("Prefabs/BeholderPBRDefault") as GameObject);
        monsterPrefabs.Add(Resources.Load("Prefabs/CactusPBR") as GameObject);
        monsterPrefabs.Add(Resources.Load("Prefabs/ChestMonsterPBRDefault") as GameObject);
        monsterPrefabs.Add(Resources.Load("Prefabs/MushroomAngryPBR") as GameObject);
        monsterPrefabs.Add(Resources.Load("Prefabs/MushroomSmilePBR") as GameObject);
    }

    private void Start()
    {
        StartCoroutine(StartGame());
    }

    //임시 함수
    IEnumerator StartGame()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            SpawnMonster();
        }
    }

    //Spawner에 몬스터 소환
    void SpawnMonster()
    {
        int monsterNum = Random.Range(0, monsterPrefabs.Count);
        Instantiate(monsterPrefabs[monsterNum], transform);
    }
}