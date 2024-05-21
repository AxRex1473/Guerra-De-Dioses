using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitAct : MonoBehaviour
{
    public GameObject panelToActivate;
    public Button buttonToActivatePanel;

    void Start()
    {
        // Agregar un listener al botón para que llame al método ActivatePanel cuando sea presionado
        buttonToActivatePanel.onClick.AddListener(ActivatePanel);
    }

    public void ActivatePanel()
    {
        // Activa el panel especificado
        panelToActivate.SetActive(true);
    }
}
