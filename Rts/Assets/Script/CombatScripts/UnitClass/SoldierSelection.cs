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

    private const int groundLayer = 7;

    void Start()
    {
        mainCamera = Camera.main;
        selectionBox.gameObject.SetActive(false);
        militaryBaseButtons.SetActive(false); // Asegurarse de que los botones estén ocultos al inicio
    }

    void Update()
    {
        HandleMouseInput();
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnLeftMouseButtonDown();
        }

        if (Input.GetMouseButton(0))
        {
            UpdateSelectionBox(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnLeftMouseButtonUp();
        }

        if (Input.GetMouseButtonUp(1) && selectedSoldiers.Count > 0)
        {
            OnRightMouseButtonUp();
        }
    }

    void OnLeftMouseButtonDown()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 1500, targetLayer | militaryLayer))
        {
            Soldier clickedSoldier = hit.transform.gameObject.GetComponent<Soldier>();
            if (clickedSoldier != null && !clickedSoldier.isEnemy)
            {
                SelectSingleSoldier(clickedSoldier);
            }
            else if (IsMilitaryBase(hit.transform.gameObject))
            {
                SelectMilitaryBase();
            }
            else
            {
                StartSelectionBox(Input.mousePosition);
            }
        }
        else
        {
            StartSelectionBox(Input.mousePosition);
        }
    }

    void OnLeftMouseButtonUp()
    {
        if (selectionBox.gameObject.activeSelf)
        {
            if (!SelectSoldiers())
            {
                DeselectAllSoldiers();
                HideMilitaryBaseButtons();
            }
            selectionBox.gameObject.SetActive(false);
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 1500, targetLayer | militaryLayer))
            {
                if (hit.transform.gameObject.GetComponent<Soldier>() == null)
                {
                    DeselectAllSoldiers();
                    if (!IsMilitaryBase(hit.transform.gameObject) && hit.transform.gameObject.layer == groundLayer)
                    {
                        HideMilitaryBaseButtons();
                    }
                }
            }
            else
            {
                DeselectAllSoldiers();
                HideMilitaryBaseButtons();
            }
        }
    }

    void OnRightMouseButtonUp()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                SetSoldiersTarget(hit.collider.gameObject);
            }
            else if (hit.collider.gameObject.layer == groundLayer)
            {
                MoveSoldiersTo(hit.point);
                SetGroundMarker(hit.point);
                HideMilitaryBaseButtons();
            }
            else
            {
                DeselectAllSoldiers();
            }
        }
    }

    void SetSoldiersTarget(GameObject target)
    {
        foreach (Soldier soldier in selectedSoldiers)
        {
            soldier.target = target;
            soldier.OnSeek();
        }
    }

    void MoveSoldiersTo(Vector3 position)
    {
        foreach (Soldier soldier in selectedSoldiers)
        {
            soldier.groundPosition = position;
            soldier.OnMove();
        }
    }

    void SetGroundMarker(Vector3 position)
    {
        groundMarker.transform.position = position;
        groundMarker.GetComponent<GroundIcon>().ActivateAndResetTimer();
    }

    void StartSelectionBox(Vector2 mousePosition)
    {
        startPosition = mousePosition;
        selectionBox.gameObject.SetActive(true);
    }

    void SelectSingleSoldier(Soldier soldier)
    {
        DeselectAllSoldiers();
        selectedSoldiers.Add(soldier);
        soldier.transform.GetChild(2).gameObject.SetActive(true);
        HideMilitaryBaseButtons();
    }

    void SelectMilitaryBase()
    {
        DeselectAllSoldiers();
        ShowMilitaryBaseButtons();
    }

    bool IsMilitaryBase(GameObject obj)
    {
        return ((1 << obj.layer) & militaryLayer) != 0;
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

        DeselectAllSoldiers(); // Desselecciona todos los soldados antes de hacer una nueva selección

        foreach (Soldier soldier in FindObjectsOfType<Soldier>())
        {
            if (soldier.isEnemy) // Saltar los soldados enemigos
            {
                continue;
            }

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
