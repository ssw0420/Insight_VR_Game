using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct SpawnData {
    public float delay;
    public string type;
}

public class Spawner : MonoBehaviour
{
    //리스폰 설정 변수
    List<SpawnData> spawnDatas;

    public List<GameObject> monsterPrefabs;
    public List<AudioClip> monsterAudio;

    private void Awake()
    {
        spawnDatas = new List<SpawnData>();
    }

    public void ReadSpawnData(float delay, string type)
    {
        SpawnData spawnData = new SpawnData();
        spawnData.delay = delay;
        spawnData.type = type;
        spawnDatas.Add(spawnData);
    }

    public void Begin()
    {
        StartCoroutine(StartGame());
    }

    //임시 함수
    IEnumerator StartGame()
    {
        while(spawnDatas.Count > 0)
        {
            float delay = spawnDatas[0].delay;
            string type = spawnDatas[0].type;
            spawnDatas.RemoveAt(0);
            yield return new WaitForSeconds(delay);
            SpawnMonster(type);
        }
    }

    //Spawner에 몬스터 소환
    void SpawnMonster(string type)
    {
        int spawnType = 0;

        switch (type)
        {
            case "HealthMonster":
                spawnType = 0;
                break;
            case "Turtle":
                spawnType = 1;
                break;
            case "Cactus":
                spawnType = 2;
                break;
            case "ChestMonster":
                spawnType = 3;
                break;
            case "MushroomAngry":
                spawnType = 4;
                break;
            case "MushroomSmile":
                spawnType = 5;
                break;
            case "Boss":
                spawnType = 6;
                break;
        }

        GameObject spawnMonster = Instantiate(monsterPrefabs[spawnType], transform);
        spawnMonster.GetComponent<Monster>().SetAudio(monsterAudio[0]);
        MonsterManager.Instance.AddLiveMonsterList(spawnMonster);
    }
}