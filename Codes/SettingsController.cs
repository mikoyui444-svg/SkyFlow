using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
     public GameObject SettingsPanel;
     private void Start()
    {
        SettingsPanel.SetActive(false); // По умолчанию закрыта
    }

    public void OpenSettings()
    {
        SettingsPanel.SetActive(true); // Открыть
    }

    public void CloseSettings()
    {
        SettingsPanel.SetActive(false); // Закрыть
    }
}
