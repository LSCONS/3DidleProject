using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private IMonsterBehaivor<EnemyController> monsterBehaivor;
    // Start is called before the first frame update
    void Start()
    {
        monsterBehaivor = new MonsterMove();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetBehavior(IMonsterBehaivor<EnemyController> _monsterBehaivor)
    {
        monsterBehaivor = _monsterBehaivor;
    }
}
