using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UIResources : StatCon
{
    //Este c�digo es �nicamente es para mostrar en la UI los Recursos
    public TextMeshProUGUI Stone, Food, Native;

    void Update()
    {
        Stone.text = totalStone + " ";
        Food.text = totalFood + " ";
        Native.text = totalNative + " ";
    }
}
