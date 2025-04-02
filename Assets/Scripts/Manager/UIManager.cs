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
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] TextMeshProUGUI atk;
    [SerializeField] TextMeshProUGUI def;

    [SerializeField] TextMeshProUGUI stage;
    [SerializeField] TextMeshProUGUI money;

    [Header("Condition")]
    [SerializeField] Slider hpSlider;
    [SerializeField] Slider expSlider;
    [SerializeField] Button activeButton;
    [SerializeField] Button autoButton;
    Coroutine glowCoroutine;

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
        playerName.text = player.PlayerName;
        atk.text = player.Damage.ToString();
        def.text = player.Defence.ToString();

        stage.text = $"Stage 1";
        money.text = player.Gold.ToString();
    }

    void UpdateCondition()
    {
        hpSlider.value = player.CurrentHP / player.MaxHp;
        expSlider.value = (float)player.CurrentExp / (float)player.MaxExp;
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

    public void AutoButton(bool isAutoMode)
    {
        activeButton.gameObject.SetActive(!isAutoMode);
        autoButton.gameObject.SetActive(isAutoMode);

        if (glowCoroutine != null)
        {
            StopCoroutine(glowCoroutine);
            glowCoroutine = null;
        }

        if (isAutoMode)
        {
            glowCoroutine = StartCoroutine(GlowEffectCoroutine(autoButton));
        }
        else if (glowCoroutine != null)
        {
            SetAlpha(autoButton, 1f);
        }

    }

    private IEnumerator GlowEffectCoroutine(Button target)
    {
        float duration = 1f;
        float timer = 0f;


        CanvasGroup canvasGroup = target.gameObject.AddComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = target.gameObject.AddComponent<CanvasGroup>();
        }

        while (true)
        {
            if (canvasGroup == null)
            {
                Debug.LogWarning("CanvasGroup이 존재하지 않음");
                yield break;
            }

            timer += Time.deltaTime;
            float alpha = Mathf.PingPong(timer, duration) / duration;
            canvasGroup.alpha = Mathf.Lerp(0.5f, 1f, alpha);
            yield return null;
        }
    }

    private void SetAlpha(Button button, float alpha)
    {
        CanvasGroup canvasGroup = button.gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = alpha;
        }
    }

}
