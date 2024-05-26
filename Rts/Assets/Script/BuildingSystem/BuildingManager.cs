using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    // Variables públicas que se pueden configurar desde el inspector de Unity.
    public GameObject[] objects; // Array de objetos que se pueden construir.
    public GameObject pendingObject; // Objeto que está pendiente de ser colocado.
    public Canvas ConstructionUI; // Interfaz de usuario para la construcción.

    private Vector3 pos; // Posición calculada del objeto a colocar.

    [SerializeField] private LayerMask LayerMask; // Capa de colisión para el raycast.

    // Referencia al script 'LoadBuildings' para guardar datos de las estructuras.
    [SerializeField] private LoadBuildings loadBuilding;

    public float gridSize; // Tamaño de la cuadrícula para alinear los objetos.

    void Update()
    {
        // Alternar la visibilidad de la interfaz de construcción al presionar la tecla R.
        if (Input.GetKeyDown(KeyCode.R))
        {
            ConstructionUI.gameObject.SetActive(!ConstructionUI.gameObject.activeSelf);
        }

        // Si hay un objeto pendiente de colocar.
        if (pendingObject != null)
        {
            // Ajustar la posición del objeto a la cuadrícula más cercana.
            pendingObject.transform.position = new Vector3(
                RoundToNearestGrid(pos.x),
                RoundToNearestGrid(pos.y),
                RoundToNearestGrid(pos.z)
            );

            // Colocar el objeto al hacer clic con el botón izquierdo del ratón.
            if (Input.GetMouseButtonDown(0))
            {
                PlaceObject();
            }
        }
    }

    // Método para colocar el objeto pendiente.
    public void PlaceObject()
    {
        // El objeto pendiente se agrega a una lista en 'LoadBuildings' (comentado).
        // LoadBuildings.estructureObjects.Add(pendingObject);
        pendingObject = null; // El objeto pendiente se resetea a null.
    }

    private void FixedUpdate()
    {
        // Lanzar un rayo desde la cámara a la posición del ratón.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Si el rayo golpea algo en la capa especificada.
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask))
        {
            pos = hit.point; // Guardar la posición del golpe.
        }
    }

    // Método para seleccionar un objeto del array para colocar.
    public void SelectedObject(int index)
    {
        pendingObject = Instantiate(objects[index], pos, transform.rotation); // Instanciar el objeto en la posición calculada.
    }

    // Método para redondear una posición a la cuadrícula más cercana.
    float RoundToNearestGrid(float pos)
    {
        float xDiff = pos % gridSize; // Calcular la diferencia con el tamaño de la cuadrícula.
        pos -= xDiff; // Ajustar la posición restando la diferencia.
        if (xDiff > (gridSize / 2)) // Si la diferencia es mayor que la mitad de la cuadrícula.
        {
            pos += gridSize; // Ajustar la posición sumando una cuadrícula completa.
        }
        return pos; // Devolver la posición ajustada.
    }
}
