using UnityEngine;
using UnityEngine.UI;

public class Selection : MonoBehaviour
{
    public GameObject selectedObject;
    private BuildingManager buildingManager;
    public GameObject objUi;
    public Image targetImage;

    void Start()
    {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
    }

    // asi ya funciona bien
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //Este Script lo tengo que cambiar para que acepte multiples tags las cuales son necesarias para Resources.Load en LoadGame
                if (hit.collider.gameObject.CompareTag("Estructure")||(hit.collider.gameObject.CompareTag("")))
                {
                    Select(hit.collider.gameObject);
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

    private void Deselect()
    {        
        objUi.SetActive(false);
        selectedObject = null;
    }

    public void Move()
    {
        buildingManager.pendingObject = selectedObject;
    }

    public void Delete()
    {
        objUi.SetActive(false);
        GameObject objToDestroy = selectedObject;
        Deselect();
        Destroy(objToDestroy);
    }
}
