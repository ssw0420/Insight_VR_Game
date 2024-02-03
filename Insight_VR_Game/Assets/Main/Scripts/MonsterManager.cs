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

    public List<Spawner> spawner;
    List<GameObject> liveMonster;
    int stage;

    //임시 게임 시작
    private void Start()
    {
        GameStart();
        liveMonster = new List<GameObject>();
    }

    public void GameStart()
    {
        stage = 1;
        ReadSpawnFile(stage);
    }

    void ReadSpawnFile(int stage)
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

        if (this.liveMonster.Count <= 0){
            stage += 1;
            ReadSpawnFile(stage);
        }
           
    }
}
