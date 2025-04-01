using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] MenuController pauseMenu;
    [SerializeField] MenuController settingsMenu;
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
        pauseMenu.MoveToTarget();
    }

    public void MoveSettingsMenu()
    {
        isSettings = !isSettings;
        settingsMenu.MoveToTarget();
    }
}
