using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSelection : MonoBehaviour
{
    private Camera mainCamera;
    public LayerMask targetLayer;
    public LayerMask militaryLayer; // Capa para las "MilitaryBases"
    public GameObject groundMarker;
    public RectTransform selectionBox;
    private Vector2 startPosition;

    private List<Soldier> selectedSoldiers = new List<Soldier>();

    public GameObject militaryBaseButtons; // Referencia a los botones de la base militar

    void Start()
    {
        mainCamera = Camera.main;
        selectionBox.gameObject.SetActive(false);
        militaryBaseButtons.SetActive(false); // Asegurarse de que los botones estén ocultos al inicio
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 500, targetLayer | militaryLayer))
            {
                Soldier clickedSoldier = hit.transform.gameObject.GetComponent<Soldier>();
                if (clickedSoldier != null)
                {
                    DeselectAllSoldiers();
                    selectedSoldiers.Add(clickedSoldier);
                    clickedSoldier.transform.GetChild(2).gameObject.SetActive(true);
                    HideMilitaryBaseButtons(); // Ocultar los botones de la base militar si un soldado es seleccionado
                }
                else if (((1 << hit.transform.gameObject.layer) & militaryLayer) != 0) // Verificar si el objeto pertenece a la capa de "MilitaryBase"
                {
                    DeselectAllSoldiers();
                    ShowMilitaryBaseButtons(); // Mostrar los botones de la base militar si la base es seleccionada
                }
                else
                {
                    startPosition = Input.mousePosition;
                    selectionBox.gameObject.SetActive(true);
                }
            }
            else
            {
                startPosition = Input.mousePosition;
                selectionBox.gameObject.SetActive(true);
            }
        }

        if (Input.GetMouseButton(0))
        {
            UpdateSelectionBox(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (selectionBox.gameObject.activeSelf)
            {
                bool soldiersSelected = SelectSoldiers();
                if (!soldiersSelected)
                {
                    DeselectAllSoldiers();
                    HideMilitaryBaseButtons(); // Ocultar los botones de la base militar si no se seleccionaron soldados
                }
                selectionBox.gameObject.SetActive(false);
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 500, targetLayer | militaryLayer))
                {
                    Soldier clickedSoldier = hit.transform.gameObject.GetComponent<Soldier>();
                    if (clickedSoldier == null)
                    {
                        DeselectAllSoldiers();
                        if (((1 << hit.transform.gameObject.layer) & militaryLayer) == 0 && hit.transform.gameObject.layer == 7)
                        {
                            HideMilitaryBaseButtons(); // Ocultar los botones de la base militar si se hace clic en el suelo
                        }
                    }
                }
                else
                {
                    DeselectAllSoldiers();
                    HideMilitaryBaseButtons(); // Ocultar los botones de la base militar si se hace clic en otro lugar
                }
            }
        }

        if (Input.GetMouseButtonUp(1) && selectedSoldiers.Count > 0)
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    foreach (Soldier soldier in selectedSoldiers)
                    {
                        soldier.target = hit.collider.gameObject;
                        soldier.OnSeek();
                    }
                }
                else if (hit.collider.gameObject.layer == 7)
                {
                    foreach (Soldier soldier in selectedSoldiers)
                    {
                        soldier.groundPosition = hit.point;
                        soldier.OnMove();
                    }
                    groundMarker.transform.position = hit.point;
                    groundMarker.GetComponent<GroundIcon>().ActivateAndResetTimer();
                    HideMilitaryBaseButtons(); // Ocultar los botones de la base militar si se hace clic en el suelo
                }
                else
                {
                    DeselectAllSoldiers();
                }
            }
        }
    }

    void UpdateSelectionBox(Vector2 currentMousePosition)
    {
        float width = currentMousePosition.x - startPosition.x;
        float height = currentMousePosition.y - startPosition.y;

        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.anchoredPosition = startPosition + new Vector2(width / 2, height / 2);
    }

    bool SelectSoldiers()
    {
        Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

        bool anySoldierSelected = false;

        foreach (Soldier soldier in FindObjectsOfType<Soldier>())
        {
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(soldier.transform.position);

            if (screenPosition.x >= min.x && screenPosition.x <= max.x && screenPosition.y >= min.y && screenPosition.y <= max.y)
            {
                selectedSoldiers.Add(soldier);
                soldier.transform.GetChild(2).gameObject.SetActive(true);
                anySoldierSelected = true;
            }
        }

        return anySoldierSelected;
    }

    void DeselectAllSoldiers()
    {
        foreach (Soldier soldier in selectedSoldiers)
        {
            soldier.transform.GetChild(2).gameObject.SetActive(false);
        }
        selectedSoldiers.Clear();
    }

    void ShowMilitaryBaseButtons()
    {
        militaryBaseButtons.SetActive(true);
    }

    void HideMilitaryBaseButtons()
    {
        militaryBaseButtons.SetActive(false);
    }
}
