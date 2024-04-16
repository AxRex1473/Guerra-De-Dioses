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
        try
        {
            string filePath = Application.persistentDataPath + "/player-Buildings.json";
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                _buildingsData = JsonUtility.FromJson<BuildingsData>(jsonData);

                foreach (var buildingInfo in _buildingsData.Buildings)
                {
                    // Search for prefabs that contain the specified name as a substring
                    GameObject prefab = FindPrefabByPartialName(buildingInfo.name);
                    if (prefab != null)
                    {
                        Vector3 position = JsonConvert.DeserializeObject<Vector3>(buildingInfo.position);
                        Quaternion rotation = JsonConvert.DeserializeObject<Quaternion>(buildingInfo.rotation);
                        Instantiate(prefab, position, rotation);
                    }
                    else
                    {
                        Debug.LogError("Prefab not found: " + buildingInfo.name);
                    }
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

    private GameObject FindPrefabByPartialName(string partialName)
    {
        GameObject[] prefabs = Resources.LoadAll<GameObject>("Prefabs/Estructuras");
        foreach (var prefab in prefabs)
        {
            // Check if the name of the prefab contains the specified partial name
            if (prefab.name.Contains(partialName))
            {
                return prefab;
            }
        }
        return null; // Return null if no matching prefab is found
    }

}
