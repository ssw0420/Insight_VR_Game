using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public List<Spawner> spawner;

    //임시 게임 시작
    private void Start()
    {
        GameStart();
    }

    public void GameStart()
    {
        int stage = GameManager.Instance.GetStage();
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
}
