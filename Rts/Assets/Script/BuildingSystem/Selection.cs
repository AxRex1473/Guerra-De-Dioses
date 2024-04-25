using UnityEngine;
using UnityEngine.UI;

public class Selection : MonoBehaviour
{
    public GameObject selectedObject;
    private BuildingManager buildingManager;
    public GameObject objUi;
    public Image targetImage;
    public GameObject aldeanoPanel;

    void Start()
    {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
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
        targetImage.sprite = obj.GetComponentInChildren<Image>().sprite;
        objUi.SetActive(true);
        selectedObject = obj;
    }

    private void SelectAldeano(GameObject obj)
    {
        if (obj == selectedObject) return;
        if (selectedObject != null) Deselect();
        // Desactivar la imagen si estaba activa
        targetImage.gameObject.SetActive(false);
        // Activar el objeto 3D del panel aldeano si estaba desactivado
        if (!aldeanoPanel.activeSelf)
        {
            aldeanoPanel.SetActive(true);
        }
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


    public void Rotate()
    {
        if (selectedObject != null)
        {
            selectedObject.transform.Rotate(0, 45, 0);
        }
    }
}