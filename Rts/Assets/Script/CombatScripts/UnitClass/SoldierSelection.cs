using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSelection : MonoBehaviour
{
    private Camera mainCamera;
    private bool isSelected = false;
    public LayerMask targetLayer;
    private Soldier selectedSoldier;
    public GameObject groundTarget;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 500, targetLayer))
            {
                selectedSoldier = hit.transform.gameObject.GetComponent<Soldier>();
                if (selectedSoldier != null) // Verificar si el objeto golpeado es un soldado
                {
                    isSelected = true;
                    Debug.Log(selectedSoldier.gameObject.name + " seleccionado.");
                }
            }
        }
        if (Input.GetMouseButtonUp(1) && isSelected && selectedSoldier != null)
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    selectedSoldier.target = hit.collider.gameObject;
                    Debug.Log("lohace");
                    //selectedSoldier.targetsDetected.Add(hit.collider.gameObject);
                    //selectedSoldier.groundPosition = hit.point;
                    //selectedSoldier.OnMove();
                    isSelected = false;
                    selectedSoldier = null; 
                }
            }
        }
    }
}
