using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public List<GameObject> bossMonsterPrefabs;
    public List<GameObject> curBossMonsters;

    public int curStage;
    public int deathCount = 0;

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

        SpawnMonster(curStage);

    }

    private void Update()
    {
        if(!CheckBossMonsterActive() && deathCount >= 20)
        {
            BossMonsterActive();
        }
        else if (!CheckMonsterActive() && deathCount < 20)
        {
            MonsterActive();
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
        GameObject bossMonster = Instantiate(bossMonsterPrefabs[0], spawners[Random.Range(2, spawners.Count)].position, Quaternion.identity);
        curBossMonsters.Add(bossMonster);
        for (int i = 0; i < curBossMonsters.Count; i++)
        {
            curBossMonsters[i].SetActive(false);
        }
    }

    private void CheckMonsterPosition()
    {
        for (int i = 0; i < curMonsters.Count; i++)
        {
            if (Vector3.Distance(curMonsters[i].transform.position, PlayerManager.Instance.PlayerTransform.position) > 30f)
            {
                curMonsters[i].GetComponent<NavMeshAgent>().enabled = false;
                curMonsters[i].transform.position = spawners[Random.Range(2, spawners.Count)].position;
                curMonsters[i].GetComponent<NavMeshAgent>().enabled = true;
            }
        }

        for (int i = 0; i < curBossMonsters.Count; i++)
        {
            if (Vector3.Distance(curBossMonsters[i].transform.position, PlayerManager.Instance.PlayerTransform.position) > 30f)
            {
                curBossMonsters[i].GetComponent<NavMeshAgent>().enabled = false;
                curBossMonsters[i].transform.position = spawners[Random.Range(2, spawners.Count)].position;
                curBossMonsters[i].GetComponent<NavMeshAgent>().enabled = true;
            }
        }
    }

    private bool CheckMonsterActive()
    {
        for (int i = 0; i < curMonsters.Count; i++)
        {
            if (curMonsters[i].activeSelf == true)
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckBossMonsterActive()
    {
        for (int i = 0; i < curBossMonsters.Count; i++)
        {
            if (curBossMonsters[i].activeSelf == true)
            {
                return true;
            }
        }
        return false;
    }

    private void MonsterActive()
    {
        for (int i = 0; i < curMonsters.Count; i++)
        {
            curMonsters[i].GetComponent<NavMeshAgent>().enabled = false;
            curMonsters[i].transform.position = spawners[Random.Range(2, spawners.Count)].position;
            curMonsters[i].GetComponent<NavMeshAgent>().enabled = true;
            curMonsters[i].GetComponent<Enemy>().isDead = false;
            curMonsters[i].GetComponent<Enemy>().Init();
            curMonsters[i].GetComponent<Enemy>().SetStatus(curStage);
            curMonsters[i].SetActive(true);
        }
    }

    private void BossMonsterActive()
    {
        // 보스몬스터가 여러마리 일 경우 랜덤으로 몬스터 하나의 정보를 가져와서 활성화로 로직 변경해야함
        for (int i = 0; i < curBossMonsters.Count; i++)
        {
            curBossMonsters[i].GetComponent<NavMeshAgent>().enabled = false;
            curBossMonsters[i].transform.position = spawners[Random.Range(2, spawners.Count)].position;
            curBossMonsters[i].GetComponent<NavMeshAgent>().enabled = true;
            curBossMonsters[i].SetActive(true);
            curBossMonsters[i].GetComponent<Enemy>().isDead = false;
            curBossMonsters[i].GetComponent<Enemy>().Init();
            curBossMonsters[i].GetComponent<Enemy>().SetStatus(curStage);
        }
    }

}
