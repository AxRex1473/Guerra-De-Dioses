using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelkeys : MonoBehaviour
{
    public GameObject panel;
    public GameObject panelControls;
    private bool isPanelActive = false;
    [SerializeField] private AudioClip _clickBtn;
    [SerializeField] private AudioSource _mainUIAudio;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PanelVillagers();
        }            

        if (Input.GetKeyDown(KeyCode.C))
        {
            PanelControlers();
        }
    }

    public void PanelVillagers()
    {                
            isPanelActive = !isPanelActive;
            panel.SetActive(isPanelActive);            
    }
    
    public void PanelControlers()
    {
        isPanelActive = !isPanelActive;
        panelControls.SetActive(isPanelActive);
        _mainUIAudio.PlayOneShot(_clickBtn);
    }
}
