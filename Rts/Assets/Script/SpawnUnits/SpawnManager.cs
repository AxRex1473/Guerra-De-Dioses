using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    int totalNatives;
    float totalHouses;

    // Este código existe para hacer que se cargue la cantidad de nativos dentro del JSON, la cantidad exacta de cuántos nativos por casa se tienen que generar se calcula en Spawn Script
    void Start() //Solo funciona el Script si esta en Start
    {

       
        if (LoadGame.loadGameDone)
        {
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

            //Hace la división y llama a la función de SpawnAllNatives de acuerdo al resultado de la división de Nativos entre las casas disponibles
            float manyNatives = totalNatives / totalHouses;
            int nativesPerHouse=Mathf.RoundToInt(manyNatives);

            foreach (Spawn spawnScript in spawnScripts)
            {

                spawnScript.SpawnAllNatives(nativesPerHouse);
            
            }
        }

    }

}
