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
        //LoadBuildings.estructureObjects.Add(pendingObject);
        //Aquí añado la estructura a mi loadBuildings
        BuildingsInfo buildingData = new BuildingsInfo();
        buildingData.name = pendingObject.name;
        //Tengo que obtener una función que solo me saque los datos que necesito, o sea el X,Y,Z de la estructura ya que si hay transform.pos hay un reference loop

        string jsonpos = JsonConvert.SerializeObject(pendingObject.transform.position, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });

        string jsonRot = JsonConvert.SerializeObject(pendingObject.transform.rotation, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });
        buildingData.position = jsonpos;
        buildingData.rotation = jsonRot;
        LoadBuildings.buildingsData.Buildings.Add(buildingData);
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
}
