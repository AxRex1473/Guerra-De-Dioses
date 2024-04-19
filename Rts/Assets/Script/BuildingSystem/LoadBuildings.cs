using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Device;

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
    public string tag;
}

public class LoadBuildings : MonoBehaviour
{
    public Tags[] _estructureArray;
    //Esta parte del código es para salvar todas las estructuras del juego al iniciar el juego.
    public static BuildingsData buildingsData = new BuildingsData();
    public void Start()
    {
        // Busca todos los objetos con tag Estructure
        //Aquí también tengo que cambiarlo para que busque los objetos con diferentes tags.

        //_estructureArray = FindGameObjectsWithTag(new string[] {"CasaEstructure","TemploEstructure","GranjaEstructure" });
        //GameObject[] estructureArray = GameObject.FindGameObjectsWithTag("Estructure");

        _estructureArray = FindObjectsOfType<Tags>();
        //Falta encontrar un método por el que reemplace el FindGameObjectsWithTag y que funcione con el nuevo sistema de Tags
        /*if (_estructureArray.TryGetComponent<Tags>(out var tags))
        {
            Debug.Log(tags.All.Select(t => t.Name));
            Debug.Log($"Has Casa{tags.HasTags("Casa")}");
        }*/


        foreach (Tags obj in _estructureArray)
        {
            BuildingsInfo buildingData = new BuildingsInfo();
            buildingData.name = obj.name;
            buildingData.tag= obj.tag;
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


