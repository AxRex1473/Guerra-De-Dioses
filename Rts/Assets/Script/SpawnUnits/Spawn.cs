using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject nativePrefab;
    public Transform spawnPoint;
    private int spawnIndex = 3;

    private void Awake()
    {
        //Con esto hago que al iniciar el juego se cargue el total de nativos que tenga en el Archivo JSON
        //SpawnTotalNatives(StatCon.totalNative);
    }

    void Start()
    {
        StartCoroutine(NativeSpawn());
    }

    IEnumerator NativeSpawn()
    {

        for (int i = 0;i < spawnIndex;i ++)
        {           
            Vector3 pos = spawnPoint.position;
            Quaternion rot = spawnPoint.rotation;
            Instantiate(nativePrefab, pos, rot);
            yield return new WaitForSeconds(3);
            //StartCoroutine(NativeSpawn());
        }        
    }

    private void SpawnTotalNatives(int NumberofNatives)
    {
        for (int i = 0; i < NumberofNatives; i++)
        {
            Vector3 pos = spawnPoint.position;
            Quaternion rot = spawnPoint.rotation;
            Instantiate(nativePrefab, pos, rot);
        }
    }
}
