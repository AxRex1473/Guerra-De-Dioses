using UnityEngine;
using UnityEngine.UI;

public class Selection_U : MonoBehaviour
{
    public GameObject selectedObject;       // El objeto seleccionado actualmente
    private BuildingManager buildingManager; // Referencia al manager de edificios
    public GameObject objUi;                // Panel de UI para mostrar información del objeto seleccionado
    public Image targetImage;               // Imagen para mostrar la apariencia del objeto seleccionado
    public GameObject aldeanoPanel;         // Panel para mostrar información específica de los aldeanos

    void Start()
    {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>(); // Obtener referencia al manager de edificios
    }

    void Update()
    {
        // Detectar clic izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Lanzar un rayo desde la cámara hacia la posición del mouse
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // Si el rayo colisiona con un objeto que tiene la etiqueta "Estructure", seleccionarlo
                if (hit.collider.gameObject.CompareTag("Estructure"))
                {
                    Select(hit.collider.gameObject);
                }
                // Si el rayo colisiona con un objeto que tiene la etiqueta "Aldeano", seleccionarlo
                else if (hit.collider.gameObject.CompareTag("Aldeano"))
                {
                    SelectAldeano(hit.collider.gameObject);
                }
            }
        }

        // Detectar clic derecho para deseleccionar
        if (Input.GetMouseButtonDown(1) && selectedObject != null)
        {
            Deselect();
        }
    }

    private void Select(GameObject obj)
    {
        if (obj == selectedObject) return;    // Si el objeto ya está seleccionado, no hacer nada
        if (selectedObject != null) Deselect(); // Si hay un objeto seleccionado, deseleccionarlo
        targetImage.sprite = obj.GetComponentInChildren<Image>().sprite; // Actualizar la imagen de la UI con la imagen del objeto seleccionado
        objUi.SetActive(true);                // Mostrar el panel de UI
        selectedObject = obj;                 // Establecer el objeto seleccionado como el objeto actual
    }

    private void SelectAldeano(GameObject obj)
    {
        if (obj == selectedObject) return;    // Si el objeto ya está seleccionado, no hacer nada
        if (selectedObject != null) Deselect(); // Si hay un objeto seleccionado, deseleccionarlo
        targetImage.gameObject.SetActive(false); // Ocultar la imagen de la UI
        if (!aldeanoPanel.activeSelf)         // Si el panel de aldeano está desactivado, activarlo
        {
            aldeanoPanel.SetActive(true);
        }
        selectedObject = obj;                 // Establecer el objeto seleccionado como el objeto actual
    }

    private void Deselect()
    {
        objUi.SetActive(false);               // Ocultar el panel de UI
        aldeanoPanel.SetActive(false);        // Ocultar el panel de aldeano
        selectedObject = null;                // Desseleccionar el objeto actual
    }

    public void Move()
    {
        buildingManager.pendingObject = selectedObject; // Establecer el objeto seleccionado como el objeto pendiente en el manager de edificios
    }

    public void Delete()
    {
        objUi.SetActive(false);               // Ocultar el panel de UI
        aldeanoPanel.SetActive(false);        // Ocultar el panel de aldeano
        GameObject objToDestroy = selectedObject; // Obtener referencia al objeto seleccionado
        Deselect();                            // Deseleccionar el objeto
        Destroy(objToDestroy);                // Destruir el objeto
    }

    public void Rotate()
    {
        if (selectedObject != null)
        {
            selectedObject.transform.Rotate(0, 45, 0); // Rotar el objeto seleccionado 45 grados en el eje Y
        }
    }
}
