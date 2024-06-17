using System;
using System.Collections;
using UnityEngine;
using TMPro;

[Serializable]
public class StatConData
{
    public int totalStone;
    public int totalFood;
    public int totalNative;
}

public class StatCon : MonoBehaviour
{
    public static int totalStone = 0;
    public static int totalFood = 0;
    public static int totalNative = 0;

   /*
    public TextMeshProUGUI stoneTextComponent; 
    public TextMeshProUGUI foodTextComponent;  
    public TextMeshProUGUI nativeTextComponent; 

    void Start()
    {
        if (stoneTextComponent == null || foodTextComponent == null || nativeTextComponent == null)
        {
            return;
        }

        StartCoroutine(UpdateText());
    }

    IEnumerator UpdateText()
    {
        while (true)
        {
            totalStone += 10;
            totalFood += 10;
            totalNative += 10;

            stoneTextComponent.text = " " + totalStone;
            foodTextComponent.text = " " + totalFood;
            nativeTextComponent.text = " " + totalNative;

            yield return new WaitForSeconds(10);
        }
    }*/
}
