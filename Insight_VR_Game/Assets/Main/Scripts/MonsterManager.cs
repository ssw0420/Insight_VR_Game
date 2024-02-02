using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public List<Spawner> spawner;

    //�ӽ� ���� ����
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
        //������ ���� �б�
        TextAsset textFile = Resources.Load("Stages/Stage" + stage) as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while (stringReader != null)
        {
            string line = stringReader.ReadLine();
            if (line == null)
                break;

            //������ ������ ����
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
