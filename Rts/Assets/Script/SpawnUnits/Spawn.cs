using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject nativePrefab;
    public Transform spawnPoint;
    private int _spawnIndex = 1;
    private int _totalNativesSpawned = 0;

    // Flag to check if natives have been spawned Este lo puede modificar el SpawnManager
    [SerializeField] private bool _nativesSpawnedAllAtOnce = false;

    //La funci�n Start sirve gracias al bool nativesSpawned... con el cu�l permite a otras casas que se a�adan adicionalmente a continuar Spawneando 
    void Start()
    {
        
        if (_nativesSpawnedAllAtOnce)
        {
            StartCoroutine(NativesSpawn());
            //SpawnAllNatives();
        }
    }

    //Esta funci�n la llamada SpawnManager y ayuda a spawnear el total de nativos que le diga el otro spawn
    public void SpawnAllNatives(float Natives)
    {
        
        if (!_nativesSpawnedAllAtOnce)
        {
            // Se calcula cu�ntos nativos en promedio se tienen que generar por SpawnScript.
            for (float i = 0; i < Mathf.Min(_spawnIndex, Natives); i++)
            {
                Vector3 pos = spawnPoint.position;
                Quaternion rot = spawnPoint.rotation;
                Instantiate(nativePrefab, pos, rot);
                _totalNativesSpawned++;
                
            }
            StartCoroutine(NativesSpawn());                        
        }
      
    }

    //De esta forma todas las casas que faltan por spawnear nativos o casas nuevas puedan seguir spawneando nativos.
    IEnumerator NativesSpawn()
    {
        //Primero se activa aqu� el booleano y no en otro lado ya que puede conflictuar con el Start que requiere el booleano,
        //As� que la l�gica es Spawnamager llama SpawnAll... despu�s se activa esta corrutina y de ah� se activa el booleano y por �ltimo si hay espacio para nativos se crean nativos     
        while (_totalNativesSpawned < _spawnIndex)
        {
            yield return new WaitForSeconds(3);
            Vector3 pos = spawnPoint.position;
            Quaternion rot = spawnPoint.rotation;
            Instantiate(nativePrefab, pos, rot);
            _totalNativesSpawned++;            
            Debug.Log("Spawneando");
        }
        //Ahora la l�gica del booleano de _nativesSpawned se maneja en un if por separado para no evitar que se generen de m�s o no se generen nativos.
        //Se spawnean de m�s si tengo el booleano afuera del while, y no se spawnean nativos si lo tengo adentro del while.
        if(_totalNativesSpawned>=_spawnIndex)
        {
            _nativesSpawnedAllAtOnce = true;
        }
    }

    // Pone el booleano como verdadero de forma externa, desde SpawnManager.
    public void SetNativesSpawnedAllAtOnce(bool value)
    {
        _nativesSpawnedAllAtOnce = value;
    }
}