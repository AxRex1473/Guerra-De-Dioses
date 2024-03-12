using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject nativePrefab;
    public Transform spawnPoint;
    private int spawnIndex = 3;

    void Start()
    {
        StartCoroutine(NativeSpawn());
    }

    IEnumerator NativeSpawn()
    {
        for (int i = 0;i < spawnIndex;i ++)
        {
            yield return new WaitForSeconds(3);
            Vector3 pos = spawnPoint.position;
            Quaternion rot = spawnPoint.rotation;
            GameObject native = Instantiate(nativePrefab, pos, rot);
            //StartCoroutine(NativeSpawn());
        }
        
    }
}
