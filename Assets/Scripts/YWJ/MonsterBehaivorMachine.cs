using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaivorMachine<T>
{
    private T monsterController;
    
    public IMonsterBehaivor<T> currentBehavior;

    public MonsterBehaivorMachine(T _monsterController, IMonsterBehaivor<T> currentBehavior)
    {
        monsterController = _monsterController;
        SetState(currentBehavior);
    }

    public void SetState(IMonsterBehaivor<T> behavior)
    {
        if (monsterController == null)
        {
            Debug.Log("monsterContoller가 없습니다.");
            return;
        }
        if (currentBehavior == behavior)
        {
            Debug.Log("monster행동이 일치합니다.");
            return;
        }
        if (currentBehavior != null)
        {
            currentBehavior.EnterBehaivor(monsterController);
        }

        currentBehavior = behavior;

        if (currentBehavior != null)
            currentBehavior.EnterBehaivor(monsterController);
    }

    public void UpdateState()
    {
        if (monsterController == null)
            return;
        currentBehavior.UpdateBehavior(monsterController);
    }
}
