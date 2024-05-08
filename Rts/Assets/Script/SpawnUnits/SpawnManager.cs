using UnityEngine;
using System.IO;


public class SpawnManager : MonoBehaviour
{
    int totalNatives;
    int totalHouses;
    int positiveRemainder;

    // Este código existe para hacer que se cargue la cantidad de nativos dentro del JSON, la cantidad exacta de cuántos nativos por casa se tienen que generar se calcula en Spawn Script
    void Start() //Solo funciona el Script si esta en Start
    {       
        if (LoadGame.loadGameDone)
        {
            //Esto es por si no tiene el JSON file, útil para el inicio de una partida sin datos guardados
            string filePath = Application.persistentDataPath + "/player-Buildings.json";
            if (!File.Exists(filePath))
            {
                // Set _nativesSpawnedAllAtOnce to true for all Spawn scripts
                SetAllSpawnScriptsNativesSpawnedAllAtOnce(true);
                return;
            }
            //Empieza buscando la cantidad de casas precargadas en eljuego
            Spawn[] spawnScripts = FindObjectsOfType<Spawn>();

            
            if(spawnScripts.Length == 0 )
            {
                
                return;
            }

            totalHouses = spawnScripts.Length; 
            totalNatives = StatCon.totalNative;  //Obtiene la cantidad de nativos

            //Si tiene 0 nativos almacenados entonces se pone en true para que pueda seguir spawneando
            if ( totalNatives == 0 )
            {
                SetAllSpawnScriptsNativesSpawnedAllAtOnce(true);
                return;
            }

            int manyNatives = (totalNatives / totalHouses);
            //Hace la división y llama a la función de SpawnAllNatives de acuerdo al resultado de la división de Nativos entre las casas disponibles
            positiveRemainder = totalNatives % totalHouses;

            //int nativesPerHouse=Mathf.RoundToInt(manyNatives);
            foreach (Spawn spawnScript in spawnScripts)
            {
                if (positiveRemainder > 0)
                {
                    // If there is a remainder, spawn one additional native for this house
                    spawnScript.SpawnAllNatives(manyNatives + 1);
                    positiveRemainder--;
                }
                else
                {
                    spawnScript.SpawnAllNatives(manyNatives);
                }
            }
        }
    }

    // LLama a la función de SetNativesSpawned... adentro de todos los spawnOBjects para que tengan el valor positivo.
    private void SetAllSpawnScriptsNativesSpawnedAllAtOnce(bool value)
    {
        Spawn[] spawnScripts = FindObjectsOfType<Spawn>();
        foreach (Spawn spawnScript in spawnScripts)
        {
            spawnScript.SetNativesSpawnedAllAtOnce(value);
        }
    }
}
