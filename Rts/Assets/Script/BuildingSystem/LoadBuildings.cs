using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class BuildingsData
{

    public string name;
    public string position;    
    //public List<Vector3>position=new List<Vector3>();
    public string rotation;
}

public class LoadBuildings : MonoBehaviour
{
    public List<BuildingsData> estructureObjects;
    public void Start()
    {
        // Find all objects with the "Estructure" tag and add them to the list
        GameObject[] estructureArray = GameObject.FindGameObjectsWithTag("Estructure");
        foreach (GameObject obj in estructureArray)
        {
            BuildingsData buildingData = new BuildingsData();
            buildingData.name = obj.name;
            //Tengo que obtener una función que solo me saque los datos que necesito, o sea el X,Y,Z de la estructura ya que si hay transform.pos hay un reference loop
            
            string jsonpos=JsonConvert.SerializeObject(obj.transform.position,Formatting.Indented,new JsonSerializerSettings
            {
                ReferenceLoopHandling=ReferenceLoopHandling.Ignore               
            });

            string jsonRot = JsonConvert.SerializeObject(obj.transform.rotation, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
             buildingData.position =jsonpos;
            buildingData.rotation = jsonRot;
            estructureObjects.Add(buildingData);
        }
    }
}


