using Newtonsoft.Json;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    // Variables p�blicas que se pueden configurar desde el inspector de Unity.
    public GameObject[] objects; // Array de objetos que se pueden construir.
    public GameObject pendingObject; // Objeto que est� pendiente de ser colocado.
    public Canvas ConstructionUI; // Interfaz de usuario para la construcci�n.

    private Vector3 pos; // Posici�n calculada del objeto a colocar.

    [SerializeField] private LayerMask LayerMask; // Capa de colisi�n para el raycast.

    // Referencia al script 'LoadBuildings' para guardar datos de las estructuras.
    [SerializeField] private LoadBuildings loadBuilding;

    public float gridSize; // Tama�o de la cuadr�cula para alinear los objetos.

    void Update()
    {
        // Alternar la visibilidad de la interfaz de construcci�n al presionar la tecla R.
        if (Input.GetKeyDown(KeyCode.R))
        {
            ConstructionUI.gameObject.SetActive(!ConstructionUI.gameObject.activeSelf);
        }

        // Si hay un objeto pendiente de colocar.
        if (pendingObject != null)
        {
            // Ajustar la posici�n del objeto a la cuadr�cula m�s cercana.
            pendingObject.transform.position = new Vector3(
                RoundToNearestGrid(pos.x),
                RoundToNearestGrid(pos.y),
                RoundToNearestGrid(pos.z)
            );

            // Colocar el objeto al hacer clic con el bot�n izquierdo del rat�n.
            if (Input.GetMouseButtonDown(0))
            {
                PlaceObject();
            }
        }
    }

    // M�todo para colocar el objeto pendiente.
    public void PlaceObject()
    {
        SendBuildingData();
        pendingObject = null;

    }

    private void FixedUpdate()
    {
        // Lanzar un rayo desde la c�mara a la posici�n del rat�n.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Si el rayo golpea algo en la capa especificada.
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask))
        {
            pos = hit.point; // Guardar la posici�n del golpe.
        }
    }

    // M�todo para seleccionar un objeto del array para colocar.
    public void SelectedObject(int index)
    {
        pendingObject = Instantiate(objects[index], pos, transform.rotation); // Instanciar el objeto en la posici�n calculada.
    }

    // M�todo para redondear una posici�n a la cuadr�cula m�s cercana.
    float RoundToNearestGrid(float pos)
    {
        float xDiff = pos % gridSize; // Calcular la diferencia con el tama�o de la cuadr�cula.
        pos -= xDiff; // Ajustar la posici�n restando la diferencia.
        if (xDiff > (gridSize / 2)) // Si la diferencia es mayor que la mitad de la cuadr�cula.
        {
            pos += gridSize; // Ajustar la posici�n sumando una cuadr�cula completa.
        }
        return pos; // Devolver la posici�n ajustada.
    }

    private void SendBuildingData()
    {
        if (pendingObject != null)
        {
            // Checa si ya existe la estructura
            string buildingName = pendingObject.name;
            BuildingsInfo existingBuilding = LoadBuildings.buildingsData.Buildings.Find(b => b.name == buildingName);

            if (existingBuilding != null)
            {
                // Actualiza la posici�n de la estructura ya existente
                existingBuilding.position = JsonConvert.SerializeObject(pendingObject.transform.position, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                existingBuilding.rotation = JsonConvert.SerializeObject(pendingObject.transform.rotation, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
            else
            {
                // Crea nueva estructura si no existe
                BuildingsInfo buildingData = new BuildingsInfo();
                pendingObject.gameObject.name = pendingObject.transform.position.ToString();
                buildingData.name = pendingObject.name;
                buildingData.tag = pendingObject.tag;
                buildingData.position = JsonConvert.SerializeObject(pendingObject.transform.position, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                buildingData.rotation = JsonConvert.SerializeObject(pendingObject.transform.rotation, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                // Lo a�ade a la lista
                LoadBuildings.buildingsData.Buildings.Add(buildingData);
            }
        }
    }
}
