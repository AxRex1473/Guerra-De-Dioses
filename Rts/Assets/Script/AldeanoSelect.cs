using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AldeanoSelect : MonoBehaviour
{
    public GameObject panelAld;

    private void Start()
    {
        if(panelAld != null)
        {
            panelAld.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        if(panelAld != null) 
        {
            panelAld.SetActive(true);
        }
    }
}
