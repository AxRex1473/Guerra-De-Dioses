using System.Collections;
using UnityEngine;

public class Food : MonoBehaviour
{
    private int baseLv1 = 1;

    void Start()
    {
        if (gameObject.name.Contains("Granja") || gameObject.name.Contains("Chinampa_Prueba"))
        {
            StartCoroutine(foodGen());
        }
    }

    IEnumerator foodGen()
    {
        yield return new WaitForSeconds(5);
        StatCon.totalFood += 10 * baseLv1;
        StartCoroutine(foodGen());
    }
}
