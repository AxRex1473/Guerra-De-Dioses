using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSelection : MonoBehaviour
{
    private Camera mainCamera;
    public LayerMask targetLayer;
    public GameObject groundMarker;
    public RectTransform selectionBox;
    private Vector2 startPosition;

    private List<Soldier> selectedSoldiers = new List<Soldier>();

    void Start()
    {
        mainCamera = Camera.main;
        selectionBox.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 500, targetLayer))
            {
                Soldier clickedSoldier = hit.transform.gameObject.GetComponent<Soldier>();
                if (clickedSoldier != null)
                {
                    DeselectAllSoldiers();
                    selectedSoldiers.Add(clickedSoldier);
                    clickedSoldier.transform.GetChild(2).gameObject.SetActive(true);
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
                }
                selectionBox.gameObject.SetActive(false);
            }
            else
            {
                // Deseleccionar si no se seleccionó un soldado
                RaycastHit hit;
                if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 500, targetLayer))
                {
                    Soldier clickedSoldier = hit.transform.gameObject.GetComponent<Soldier>();
                    if (clickedSoldier == null)
                    {
                        DeselectAllSoldiers();
                    }
                }
                else
                {
                    DeselectAllSoldiers();
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
}
