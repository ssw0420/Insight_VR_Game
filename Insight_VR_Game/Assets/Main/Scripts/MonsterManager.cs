using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    protected static MonsterManager instance;

    public static MonsterManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    public List<Transform> bossFinishPoint;
    public List<Spawner> spawner;
    public GameObject HealthPotion;
    public Material hitMaterial;
    List<GameObject> liveMonster;
    [Header("Stage Info")]
    [SerializeField]int maxStage;
    [SerializeField]int stage;
    

    //임시 게임 시작
    private void Start()
    {
        GameStart();
        liveMonster = new List<GameObject>();
    }

    public void GameStart()
    {
        ReadSpawnFile();
        GameManager.Instance.StartTimeCount();
    }

    public void ReadSpawnFile()
    {
        //리스폰 파일 읽기
        TextAsset textFile = Resources.Load("Stages/Stage" + stage) as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while (stringReader != null)
        {
            string line = stringReader.ReadLine();
            if (line == null)
                break;

            //리스폰 데이터 생성
            float delay = float.Parse(line.Split(",")[0]);
            string type = line.Split(",")[1];
            int point = int.Parse(line.Split(",")[2]);
            spawner[point].ReadSpawnData(delay, type);
        }

        foreach (Spawner spawn in spawner)
        {
            spawn.Begin();
        }

        stringReader.Close();
    }

    public void AddLiveMonsterList(GameObject liveMonster)
    {
        this.liveMonster.Add(liveMonster);
    }

    public void DeleteLiveMonsterList(GameObject liveMonster)
    {
        this.liveMonster.Remove(liveMonster);

        if (this.liveMonster.Count <= 0)
        {
            if (stage >= maxStage)
            {
                GameManager.Instance.GameVictroy();
                return;
            }

            stage += 1;
            StartCardTest.instance.ShowCard();
        }
    }

    public void MonsterWin()
    {
        foreach(GameObject monster in liveMonster)
        {
            monster.GetComponent<Monster>().Win();
        }
    }

    public List<Transform> GetBossPointList()
    {
        return bossFinishPoint;
    }

    public GameObject GetHealthPotionPrefab()
    {
        return HealthPotion;
    }

    public Material GetHitMaterial()
    {
        return hitMaterial;
    }
}