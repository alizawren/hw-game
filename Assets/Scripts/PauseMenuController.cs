using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public GameController controller;
    public GameObject panel;
    public GameObject menuPause;
    public SettingsMenu settings;
    void Start()
    {
        panel.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
    }

    public void OpenMenu()
    {
        menuPause.SetActive(true);
    }

    public void CloseMenu ()
    {
        menuPause.SetActive(false);
        settings.SavePreferences();
    }
}
