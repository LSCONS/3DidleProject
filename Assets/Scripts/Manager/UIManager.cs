using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField] Player player;
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] TextMeshProUGUI name;
    [SerializeField] TextMeshProUGUI atk;
    [SerializeField] TextMeshProUGUI def;

    [SerializeField] TextMeshProUGUI stage;
    [SerializeField] TextMeshProUGUI money;

    [Header("Condition")]
    [SerializeField] Slider hpSlider;
    [SerializeField] Slider expSlider;

    [Header("Skill")]

    [Header("Settings")]
    [SerializeField] MenuController pauseMenu;
    [SerializeField] MenuController settingsMenu;
    [SerializeField] Image fade;
    bool isFade;
    bool isSettings;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isSettings)
                MoveSettingsMenu();
            else
                MovePauseMenu();
        }
    }

    private void LateUpdate()
    {
        UpdateStat();
        UpdateCondition();
    }

    void UpdateStat()
    {
        level.text = $"Lv.{player.Level}";
        name.text = player.name;
        atk.text = player.Damage.ToString();
        def.text = player.Defence.ToString();

        stage.text = $"Stage 1";
        money.text = player.Gold.ToString();
    }

    void UpdateCondition()
    {
        hpSlider.value = player.MaxHp / player.CurrentHP;
        expSlider.value = player.MaxExp / player.CurrentExp;
    }

    public void MovePauseMenu()
    {
        if (!isSettings)
        {
            StopCoroutine(FadeInOut(isFade));
            isFade = !isFade;
            fade.gameObject.SetActive(isFade);
            StartCoroutine(FadeInOut(isFade));
            pauseMenu.MoveToTarget();
        }
    }

    public void MoveSettingsMenu()
    {
        isSettings = !isSettings;
        settingsMenu.MoveToTarget();
    }

    IEnumerator FadeInOut(bool isFade)
    {
        if (isFade)
        {
            float fadeCount = 0;
            while (fadeCount < 0.7f)
            {
                fadeCount += 0.1f;
                yield return new WaitForSeconds(0.01f);
                fade.color = new Color(0, 0, 0, fadeCount);
            }
        }
        else
        {
            float fadeCount = 0.7f;
            while (fadeCount > 0)
            {
                fadeCount -= 0.1f;
                yield return new WaitForSeconds(0.01f);
                fade.color = new Color(0, 0, 0, fadeCount);
            }
        }
    }
}
