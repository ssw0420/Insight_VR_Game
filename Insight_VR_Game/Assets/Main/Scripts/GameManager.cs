using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //�׽�Ʈ �ڵ�(�ӽ� ���� ��� ����) ���߿� ���� ����
    List<Monster> monster;

    //Stage ���� ����� ����
    public List<Spawner> spawner;
    public int stage;

    private void Awake()
    {
        monster = new List<Monster>();
    }

    private void Start()
    {
        ReadSpawnFile();
    }

    void ReadSpawnFile()
    {
        //������ ���� �б�
        TextAsset textFile = Resources.Load("Stages/Stage" + stage) as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while(stringReader != null)
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

        foreach(Spawner spawn in spawner)
        {
            spawn.Begin();
        }

        stringReader.Close();
    }

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
