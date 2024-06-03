using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestingAnim : MonoBehaviour
{
    //Este código va para DinamicPauseCanvas, para que pueda hacer la función y le diga al objeto con el código PauseGame que ya terminó la animación.
    [SerializeField] private PauseGame gameUIref;

    //Una vez que termine la animación se ejecuta la Función AnimDone() en el PauseGame.
    public void AnimationDoDone()
    {
        gameUIref.AnimDone();
    }

}
