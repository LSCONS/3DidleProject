using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill_Player_ShockwaveCrush : Skill
{
    public Skill_Player_ShockwaveCrush(string name, float duration, float cooltime) : base(name, duration, cooltime)
    {

    }

    public override void RemoveSkill()
    {
        base.RemoveSkill();
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public override void UpdateSkillDuration()
    {
        base.UpdateSkillDuration();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        // 플레이어가 점프 후 바닥을 내려 찍으며 원형 스킬범위의 적들에게 피해를 입힌다.
    }

    public override void LevelUp()
    {
        base.LevelUp();
    }

}


public class Skill_Player : MonoBehaviour
{
    
}


