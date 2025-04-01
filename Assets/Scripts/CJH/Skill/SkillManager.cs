using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public enum SkillType
{
    OrbitBlade,

}

public class SkillManager : MonoBehaviour
{
    [Header("스킬 슬롯")]
    public Dictionary<SkillType, Skill> skills = new Dictionary<SkillType, Skill>();


    // 쿨타임 표시해줄 텍스트

    public bool isAutoMode = false;     // 자동사냥 모드

    private void Awake()
    {
        skills = new Dictionary<SkillType, Skill>()
        {
            {SkillType.OrbitBlade, new Skill_Player_OrbitBlades() }
        };
    }

    private void Update()
    {
        foreach (var skill in skills.Values)
        {
            skill.UpdateSkillDuration();

            // if문으로 쿨타임을 표시

            if (isAutoMode && skill.state == SkillState.Ready)
            {
                skill.UseSkill();
            }

        }

    }

    public void UseSkill(SkillType type)
    {
        if (skills.ContainsKey(type))
        {
            Skill skill = skills[type];
            if (skill.state == SkillState.Ready)
            {
                skill.UseSkill();
            }
            else
            {
                Debug.Log($"[{type}] 쿨타임 : {skill.GetCooldown()}");
            }
        }
    }

    public void OnOrbitSkill(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            UseSkill(SkillType.OrbitBlade);
        }
    }


}
