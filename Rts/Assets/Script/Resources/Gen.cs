using System.Collections;
using UnityEngine;

public class Gen : MonoBehaviour
{
    private int baseLv1 = 1;
    
    void Start()
    {
        if (gameObject.tag.Contains("MinaEstructure"))
        {
            StartCoroutine(stoneGen());
        }
    }

    IEnumerator stoneGen()
    {
        yield return new WaitForSeconds(5);
        StatCon.totalStone += 10*baseLv1;
        StartCoroutine(stoneGen());
    }
}
