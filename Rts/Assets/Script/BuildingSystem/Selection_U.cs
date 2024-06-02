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
        // Obtener referencia al manager de edificios
        buildingManager = GameObject.Find("BuildingManager")?.GetComponent<BuildingManager>();
        if (buildingManager == null)
        {
            Debug.LogError("BuildingManager no encontrado o no tiene un componente BuildingManager.");
        }
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

        // Buscar el componente Image en todos los hijos del objeto seleccionado
        Image imageComponent = obj.GetComponentInChildren<Image>();

        // Si no se encuentra el componente Image en el objeto seleccionado, buscar en BuildingSystem
        if (imageComponent == null)
        {
            Debug.Log("El objeto seleccionado no tiene un componente Image hijo. Buscando en BuildingSystem...");
            imageComponent = GameObject.Find("BuildingSystem")?.GetComponentInChildren<Image>();
        }

        if (imageComponent == null)
        {
            Debug.LogError("No se encontró un componente Image ni en el objeto seleccionado ni en BuildingSystem.");
            return;
        }

        targetImage.sprite = imageComponent.sprite; // Actualizar la imagen de la UI con la imagen del objeto seleccionado
        if (targetImage.sprite == null)
        {
            Debug.LogError("El sprite del Image es null.");
        }

        if (objUi != null)
        {
            objUi.SetActive(true);                // Mostrar el panel de UI
        }
        else
        {
            Debug.LogError("objUi es null.");
        }

        selectedObject = obj;                 // Establecer el objeto seleccionado como el objeto actual
    }

    private void SelectAldeano(GameObject obj)
    {
        if (obj == selectedObject) return;    // Si el objeto ya está seleccionado, no hacer nada
        if (selectedObject != null) Deselect(); // Si hay un objeto seleccionado, deseleccionarlo

        if (targetImage != null)
        {
            targetImage.gameObject.SetActive(false); // Ocultar la imagen de la UI
        }
        else
        {
            Debug.LogError("targetImage es null.");
        }

        if (aldeanoPanel != null && !aldeanoPanel.activeSelf)         // Si el panel de aldeano está desactivado, activarlo
        {
            aldeanoPanel.SetActive(true);
        }
        else if (aldeanoPanel == null)
        {
            Debug.LogError("aldeanoPanel es null.");
        }

        selectedObject = obj;                 // Establecer el objeto seleccionado como el objeto actual
    }

    private void Deselect()
    {
        if (objUi != null)
        {
            objUi.SetActive(false);               // Ocultar el panel de UI
        }

        if (aldeanoPanel != null)
        {
            aldeanoPanel.SetActive(false);        // Ocultar el panel de aldeano
        }

        selectedObject = null;                // Desseleccionar el objeto actual
    }

    public void Move()
    {
        if (buildingManager != null)
        {
            buildingManager.pendingObject = selectedObject; // Establecer el objeto seleccionado como el objeto pendiente en el manager de edificios
        }
    }
    
    public void Delete()
    {
        if (objUi != null)
        {
            objUi.SetActive(false);               // Ocultar el panel de UI
        }

        if (aldeanoPanel != null)
        {
            aldeanoPanel.SetActive(false);        // Ocultar el panel de aldeano
        }

        GameObject objToDestroy = selectedObject; // Obtener referencia al objeto seleccionado
        Deselect();                            // Deseleccionar el objeto
        Destroy(objToDestroy);                // Destruir el objeto
    }

    public void Rotate()
    {
        if (selectedObject != null)
        {
            selectedObject.transform.Rotate(0, 0, 45); // Rotar el objeto seleccionado 45 grados en el eje Y
        }
    }
}
