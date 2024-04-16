using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Esto es lo que se guarda en el JSON
[Serializable]
public class BuildingsData
{
    public List<BuildingsInfo> Buildings = new List<BuildingsInfo>();
}

//Esta info servirá para poder guardar en la lista de BuildingsDat
[Serializable]
public class BuildingsInfo
{       
    public string name;
    public string position;    
    public string rotation;
}

public class LoadBuildings : MonoBehaviour
{
    //Esta parte del código es para salvar todas las estructuras del juego al iniciar el juego.
    public static BuildingsData buildingsData = new BuildingsData();
    public void Start()
    {
        // Busca todos los objetos con tag Estructure
        //Aquí también tengo que cambiarlo para que busque los objetos con diferentes tags.
        GameObject[] estructureArray = GameObject.FindGameObjectsWithTag("Estructure");
        foreach (GameObject obj in estructureArray)
        {
            BuildingsInfo buildingData = new BuildingsInfo();
            buildingData.name = obj.name;
            //Tengo que obtener una función que solo me saque los datos que necesito, o sea el X,Y,Z de la estructura ya que si hay transform.pos hay un reference loop

            string jsonpos = JsonConvert.SerializeObject(obj.transform.position, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            string jsonRot = JsonConvert.SerializeObject(obj.transform.rotation, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            buildingData.position = jsonpos;
            buildingData.rotation = jsonRot;
            buildingsData.Buildings.Add(buildingData);
        }
    }
}


