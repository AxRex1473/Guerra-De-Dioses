using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelkeys : MonoBehaviour
{
    public GameObject panel; // Asigna el panel en el inspector
    private bool isPanelActive = false; // Variable para almacenar el estado del panel

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // Detectar la tecla T
        {
            // Cambiar el estado del panel
            isPanelActive = !isPanelActive;
            // Activar o desactivar el panel basado en el nuevo estado
            panel.SetActive(isPanelActive);
        }
    }
    
}
