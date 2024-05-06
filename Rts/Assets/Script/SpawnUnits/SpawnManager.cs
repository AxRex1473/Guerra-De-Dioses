using UnityEngine;
using System.IO;


public class SpawnManager : MonoBehaviour
{
    int totalNatives;
    int totalHouses;
    int positiveRemainder;

    // Este c�digo existe para hacer que se cargue la cantidad de nativos dentro del JSON, la cantidad exacta de cu�ntos nativos por casa se tienen que generar se calcula en Spawn Script
    void Start() //Solo funciona el Script si esta en Start
    {       
        if (LoadGame.loadGameDone)
        {
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

            if( totalNatives == 0 )
            {
                return;
            }

            int manyNatives = (totalNatives / totalHouses);
            //Hace la divisi�n y llama a la funci�n de SpawnAllNatives de acuerdo al resultado de la divisi�n de Nativos entre las casas disponibles
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

    // LLama a la funci�n de SetNativesSpawned... adentro de todos los spawnOBjects para que tengan el valor positivo.
    private void SetAllSpawnScriptsNativesSpawnedAllAtOnce(bool value)
    {
        Spawn[] spawnScripts = FindObjectsOfType<Spawn>();
        foreach (Spawn spawnScript in spawnScripts)
        {
            spawnScript.SetNativesSpawnedAllAtOnce(value);
        }
    }
}
