using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UIResources : StatCon
{
    //Este código es únicamente es para mostrar en la UI los Recursos
    public TextMeshProUGUI Stone, Food, Native;
    [SerializeField] private Animator StoneAnim;
    [SerializeField] private Animator FoodAnim;
    [SerializeField] private Animator NativeAnim;

    private int previousStone,previousFood,previousNative;

    private void Start()
    {
        previousStone = totalStone;
        previousFood = totalFood;
        previousNative = totalNative;
    }

    void Update()
    {
        Stone.text = totalStone + " ";
        if (totalStone != previousStone)
        {
            StoneAnim.SetTrigger("RockChange");
            // Actualiza el nuevo valor anterior
            previousStone = totalStone;
        }
        Food.text = totalFood + " ";
        if (totalFood != previousFood)
        {
            FoodAnim.SetTrigger("ComidaChange");
            previousFood = totalFood;
        }
        Native.text = totalNative + " ";
        if (totalNative != previousNative)
        {
            NativeAnim.SetTrigger("NativesChanging");
            previousNative = totalNative;
        }
    }
}
