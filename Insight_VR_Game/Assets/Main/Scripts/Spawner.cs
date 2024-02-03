using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct SpawnData {
    public float delay;
    public string type;
}

public class Spawner : MonoBehaviour
{
    //������ ���� ����
    List<SpawnData> spawnDatas;

    public List<GameObject> monsterPrefabs;

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

    //�ӽ� �Լ�
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

    //Spawner�� ���� ��ȯ
    void SpawnMonster(string type)
    {
        int spawnType = 0;

        switch (type)
        {
            case "Slime":
                spawnType = 0;
                break;
            case "Turtle":
                spawnType = 1;
                break;
            case "Beholder":
                spawnType = 2;
                break;
            case "Cactus":
                spawnType = 3;
                break;
            case "ChestMonster":
                spawnType = 4;
                break;
            case "MushroomAngry":
                spawnType = 5;
                break;
            case "MushroomSmile":
                spawnType = 6;
                break;
        }

        Instantiate(monsterPrefabs[spawnType], transform);
    }
}