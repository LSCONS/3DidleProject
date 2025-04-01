using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public enum Monsters
    {
        Skeleton,
    }
    public List<Transform> spawners = new List<Transform>();
    public int stage;
    public int monsterCount;
    public Dictionary<Monsters, GameObject> dicMonsters = new Dictionary<Monsters, GameObject>();
    public List<GameObject> monsters;
    private bool isInit = true;

    private void Start()
    {
        dicMonsters.Add(Monsters.Skeleton, monsters[0]);
    }

    private void Update()
    {
        if (isInit&& spawners != null)
        {
            SpawnMonster();
            isInit = false;
        }
    }

    private void SpawnMonster()
    {
        // 스테이지 별로 몬스터 소환갯수 조절
        // 몬스터 갯수만큼 생성
        for (int i = 0; i < monsterCount; i++)
        {
            // 몬스터 생성시 랜덤위치 스폰
            Instantiate(dicMonsters[Monsters.Skeleton], spawners[Random.Range(2, spawners.Count-1)].position, Quaternion.identity);
        }


    }
}
