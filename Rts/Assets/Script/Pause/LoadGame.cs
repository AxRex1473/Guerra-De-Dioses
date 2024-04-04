using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    // Reference to StatCon class
    public StatConData statConData;


    private void Awake()
    {
        LoadGameFile();
    }

    public void LoadGameFile()
    {
        try
        {
            string filePath = Application.persistentDataPath + "/player-Resources.json"; // Path to the JSON file
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath); // Read JSON data from file
                statConData = JsonUtility.FromJson<StatConData>(jsonData); // Deserialize JSON data into StatCon object
                //Con estos hago que el Script obtenga lo datos que tengo en el JSON
                StatCon.totalStone=statConData.totalStone;
                StatCon.totalFood= statConData.totalFood;
                //Tengo que hacer que se genere en el Script de Spawn la misma cantidad de Nativos que en el Script
                StatCon.totalNative = statConData.totalNative;
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

}
