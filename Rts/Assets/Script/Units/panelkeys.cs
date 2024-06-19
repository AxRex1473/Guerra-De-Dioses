using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelkeys : MonoBehaviour
{
    public GameObject panel;
    public GameObject panelControls;
    private bool isPanelActive = false; 


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) 
        {
            isPanelActive = !isPanelActive;
            panel.SetActive(isPanelActive);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            isPanelActive = !isPanelActive;
            panelControls.SetActive(isPanelActive);
        }
    }
    
}
