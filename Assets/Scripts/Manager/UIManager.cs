using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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
