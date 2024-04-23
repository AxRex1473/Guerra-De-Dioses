using UnityEngine;
using UnityEngine.UI;

public class Selection : MonoBehaviour
{
    public GameObject selectedObject;
    private BuildingManager buildingManager;
    public GameObject objUi;
    public GameObject aldeanoPanel;
    private Vector3 panelOffset;

    void Start()
    {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
        panelOffset = aldeanoPanel.transform.position - Camera.main.transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.CompareTag("Estructure"))
                {
                    Select(hit.collider.gameObject);
                }
                else if (hit.collider.gameObject.CompareTag("Aldeano"))
                {
                    SelectAldeano(hit.collider.gameObject);
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && selectedObject != null)
        {
            Deselect();
        }
    }

    private void Select(GameObject obj)
    {
        if (obj == selectedObject) return;
        if (selectedObject != null) Deselect();
        objUi.SetActive(true);
        objUi.transform.position = obj.transform.position;
        selectedObject = obj;
    }

    private void SelectAldeano(GameObject obj)
    {
        if (obj == selectedObject) return;
        if (selectedObject != null) Deselect();
        aldeanoPanel.SetActive(true);
        aldeanoPanel.transform.position = Camera.main.transform.position + panelOffset;
        selectedObject = obj;
    }

    private void Deselect()
    {
        objUi.SetActive(false);
        aldeanoPanel.SetActive(false);
        selectedObject = null;
    }

    public void Move()
    {
        buildingManager.pendingObject = selectedObject;
    }

    public void Delete()
    {
        objUi.SetActive(false);
        aldeanoPanel.SetActive(false);
        GameObject objToDestroy = selectedObject;
        Deselect();
        Destroy(objToDestroy);
    }
}
