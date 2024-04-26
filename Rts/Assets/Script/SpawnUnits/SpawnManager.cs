using UnityEngine;

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

            //Hace la divisi�n y llama a la funci�n de SpawnAllNatives de acuerdo al resultado de la divisi�n de Nativos entre las casas disponibles
            positiveRemainder = totalNatives % totalHouses;
            int manyNatives = Mathf.FloorToInt(totalNatives / totalHouses);
            //int nativesPerHouse=Mathf.RoundToInt(manyNatives);
            if(positiveRemainder==0 )
            {
                foreach (Spawn spawnScript in spawnScripts)
                {
                    spawnScript.SpawnAllNatives(manyNatives);
                }
            }
            
            //Podr�a hacer un booleano adentro de cada spawn para hacer que siga generando aldeanos en base al positiveRemainder 
            else
            {
                foreach (Spawn spawnScript in spawnScripts)
                {
                    spawnScript.SpawnAllNatives(positiveRemainder);
                }
            }
            
        }

    }

}
