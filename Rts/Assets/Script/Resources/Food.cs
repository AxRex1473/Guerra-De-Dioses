using System.Collections;
using UnityEngine;

public class Food : MonoBehaviour
{
    private int baseLv1 = 1;

    void Start()
    {
        if (gameObject.tag.Contains("GranjaEstructure") || gameObject.tag.Contains("ChinampaEstructure"))
        {
            Debug.Log("Aña");
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
