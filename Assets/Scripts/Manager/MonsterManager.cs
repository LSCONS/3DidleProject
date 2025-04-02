using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterManager : MonoBehaviour
{
    public enum Monsters
    {
        Skeleton,
        Slime1,
        Slime2,
        Mushroom,
        Cactus,
    }
    private static MonsterManager instance;
    public static MonsterManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("MonsterManager").AddComponent<MonsterManager>();
            }
            return instance;
        }
    }

    public List<Transform> spawners = new List<Transform>();
    private Dictionary<int, int> stages = new Dictionary<int, int>();
    private Dictionary<Monsters, GameObject> dicMonsters = new Dictionary<Monsters, GameObject>();
    public List<GameObject> monstersPrefabs;
    public List<GameObject> curMonsters;
    private bool isInit = true;

    private float lastTime = 0f;
    private float lastRate = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        stages.Add(1, 10);
        stages.Add(2, 20);

        dicMonsters.Add(Monsters.Skeleton, monstersPrefabs[0]);
        dicMonsters.Add(Monsters.Slime1, monstersPrefabs[1]);
        dicMonsters.Add(Monsters.Slime2, monstersPrefabs[2]);
        dicMonsters.Add(Monsters.Mushroom, monstersPrefabs[3]);
        dicMonsters.Add(Monsters.Cactus, monstersPrefabs[4]);

    }

    private void Update()
    {
        if (curMonsters.Count < 1)
        {
            SpawnMonster(2);
        }
        if (Time.time - lastTime > lastRate)
        {
            lastTime = Time.time;
            CheckMonsterPosition();
        }
    }

    private void SpawnMonster(int stage)
    {
        // 스테이지 별로 몬스터 소환갯수 조절
        // 몬스터 갯수만큼 생성
        for (int i = 0; i < stages[stage]; i++)
        {
            // 몬스터 생성시 랜덤위치 스폰
            GameObject monster = Instantiate(dicMonsters[(Monsters)Random.Range(0,monstersPrefabs.Count)], spawners[Random.Range(2, spawners.Count)].position, Quaternion.identity);
            curMonsters.Add(monster);
        }
    }

    private void CheckMonsterPosition()
    {
        for (int i = 0; i < curMonsters.Count; i++)
        {
            if (Vector3.Distance(curMonsters[i].transform.position, PlayerManager.Instance.PlayerTransform.position) > 20f)
            {
                curMonsters[i].GetComponent<NavMeshAgent>().enabled = false;
                curMonsters[i].transform.position = spawners[Random.Range(2, spawners.Count)].position;
                curMonsters[i].GetComponent<NavMeshAgent>().enabled = true;
            }
        }
    }
}
