using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject firstPanel;
    public GameObject secondPanel;
    public GameObject panel;
    public KeyCode activationKey;

    void Start()
    {
        // Activa el primer panel y desactiva el segundo panel al iniciar
        firstPanel.SetActive(true);
        secondPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Activa el segundo panel y desactiva el primer panel al presionar la tecla R
            firstPanel.SetActive(false);
            secondPanel.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            firstPanel.SetActive(true);
            secondPanel.SetActive(false);
        }
        if(Input.GetButtonDown("Build"))
        {
            panel.SetActive(!panel.activeSelf);
        }
    }
}
