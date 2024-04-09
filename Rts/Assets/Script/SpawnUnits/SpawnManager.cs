using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    // Este código existe para hacer que se cargue la cantidad de nativos dentro del JSON, la cantidad exacta de cuántos nativos por casa se tienen que generar se calcula en Spawn Script
    void Start() //Solo funciona el Script si esta en Start
    {
        // Get all objects with Spawn script
        Spawn[] spawnScripts = FindObjectsOfType<Spawn>();

        foreach (Spawn spawnScript in spawnScripts)
        {
            spawnScript.SpawnAllNatives(); // Spawn natives per house
        }

    }

}
