using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Device;

//Apesar de que diga LoadBuildings, este c�digo termino siendo m�s como el equivalente de StatCon para las estructuras, siendo el c�digo que recopila toda la info de las estructuras para que despu�s se guarden en PauseGame y se carguen en el LoadGame

//Esto es lo que se guarda en el JSON
[Serializable]
public class BuildingsData
{
    public List<BuildingsInfo> Buildings = new List<BuildingsInfo>();
}

//Esta info servir� para poder guardar en la lista de BuildingsDat
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
    //Esta parte del c�digo es para salvar todas las estructuras del juego al iniciar el juego.
    public static BuildingsData buildingsData = new BuildingsData();
    public void Start()
    {
        //No es necesaria la funci�n solo es para debug. Las estructuras ya se guardan en PauseGame
        FindObjects();
    }

    private void FindObjects()
    {
        // Busca todos los objetos con tag Estructure
        _estructureArray = FindObjectsOfType<Tags>();

        //Aqu� busco por cada obj que tenga el Scriptable Object Tag para que se instancien.
        foreach (Tags obj in _estructureArray)
        {
            BuildingsInfo buildingData = new BuildingsInfo();
            buildingData.name = obj.name;
            buildingData.tag = obj.tag;
            //Tengo que obtener una funci�n que solo me saque los datos que necesito, o sea el X,Y,Z de la estructura ya que si hay transform.pos hay un reference loop

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