using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] MenuController pauseMenu;
    [SerializeField] MenuController settingsMenu;
    [SerializeField] Image fade;
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
            fade.gameObject.SetActive(true);
            pauseMenu.MoveToTarget();
        }
    }

    public void MoveSettingsMenu()
    {
        isSettings = !isSettings;
        settingsMenu.MoveToTarget();
    }
}
