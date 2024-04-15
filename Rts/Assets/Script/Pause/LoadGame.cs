using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    //Aqui es donde guardo la data de los recursos y de todo lo que vaya a guardarse en el JSON
    private StatConData _statConData;
    private BuildingsData _buildingsData;


    private void Awake()
    {
        LoadGameResources();
        LoadGameBuildings();
    }

    public void LoadGameResources()
    {
        try
        {
            string filePath = Application.persistentDataPath + "/player-Resources.json"; // Path to the JSON file
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath); // Read JSON data from file
                _statConData = JsonUtility.FromJson<StatConData>(jsonData); // Deserialize JSON data into StatCon object
                //Con estos hago que el Script obtenga lo datos que tengo en el JSON
                StatCon.totalStone=_statConData.totalStone;
                StatCon.totalFood= _statConData.totalFood;
                //Tengo que hacer que se genere en el Script de Spawn la misma cantidad de Nativos que en el Script
                StatCon.totalNative = _statConData.totalNative;
                
                Debug.Log("Game data loaded successfully!");
            }
            else
            {
                Debug.LogError("File not found: " + filePath);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Could not load game data: " + e.Message);
        }
    }

    private void LoadGameBuildings()
    {
        //Tengo que hacer una función con la que pueda cargar la cantidad de estructuras y su posición.
        try
        {
            string filePath = Application.persistentDataPath + "/player-Buildings.json"; // Path to the JSON file
            if (File.Exists(filePath))
            {
                //PlayerBuildings Stuff, tengo que obtener su nombre, su posición y su rotación e instancear un prefab de dichas estrcutras si no es que ya estan isntanceadas.
                string jsonData = File.ReadAllText(filePath); // Lee JSON 
                _buildingsData= JsonUtility.FromJson<BuildingsData>(jsonData); // Desiraliza JSON
                //Con estos hago que el Script obtenga lo datos que tengo en el JSON
                foreach (var buildingInfo in _buildingsData.Buildings)
                {
                    string prefabPath = "Prefabs/Estructuras/" + buildingInfo.name;
                    Debug.Log("Attempting to load prefab: " + prefabPath);
                    // Load prefab based on name
                    GameObject prefab = Resources.Load<GameObject>("Prefabs/Estructuras/" + buildingInfo.name);
                    if (prefab != null)
                    {
                        // Se instancia prefab con la info específica del JSON
                        Vector3 position = JsonConvert.DeserializeObject<Vector3>(buildingInfo.position);
                        Quaternion rotation = JsonConvert.DeserializeObject<Quaternion>(buildingInfo.rotation);
                        Instantiate(prefab, position, rotation);
                    }
                    else
                    {
                        Debug.LogError("Prefab not found: " + buildingInfo.name);
                        Debug.LogError("Prefab not found: " + prefabPath);
                    }
                    Debug.Log("Building Data loaded successfully!");
                }
            }
            else
            {
                Debug.LogError("File not found: " + filePath);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Could not load game data: " + e.Message);
        }
    }

}
