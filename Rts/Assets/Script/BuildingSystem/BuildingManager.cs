using Newtonsoft.Json;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject pendingObject;
    public Canvas ConstructionUI;

    private Vector3 pos;

    [SerializeField] private LayerMask LayerMask;

    //Referencia del código donde podré obtener una lista de las estructuras las cuales después podré usar para guardar los datos
    [SerializeField]private LoadBuildings loadBuilding;

    public float gridSize;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            ConstructionUI.gameObject.SetActive(!ConstructionUI.gameObject.activeSelf);
        }

        if (pendingObject != null)
        {
            pendingObject.transform.position = new Vector3(
                RoundToNearestGrid(pos.x),
                RoundToNearestGrid(pos.y),
                RoundToNearestGrid(pos.z)
                );

            if (Input.GetMouseButtonDown(0))
            {
                PlaceObject();
            }
        }
    }

    public void PlaceObject()
    {
        SendBuildingData();
        pendingObject = null;

    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask))
        {
            pos = hit.point;
        }
    }

    public void SelectedObject(int index)
    {
        pendingObject = Instantiate(objects[index],pos,transform.rotation);
    }

    float RoundToNearestGrid(float pos)
    {
        float xDiff = pos % gridSize;
        pos -= xDiff;
        if(xDiff > (gridSize / 2))
        {
            pos += gridSize;
        }
        return pos;
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
                // Update the position and rotation of the existing building
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

                // Lo añade a la lista
                LoadBuildings.buildingsData.Buildings.Add(buildingData);
            }
        }
    }
}
