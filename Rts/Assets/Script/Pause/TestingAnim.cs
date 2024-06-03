using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestingAnim : MonoBehaviour
{
    //Este código va para DinamicPauseCanvas, para que pueda hacer la función y le diga al objeto con el código PauseGame que ya terminó la animación.
    [SerializeField] private PauseGame gameUIref;
    [SerializeField] private CustomAnimationsMenu animMenu;

    //Una vez que termine la animación se ejecuta la Función AnimDone() en el PauseGame.
    public void AnimationDoDone()
    {
       // if(Opti)
        //gameUIref.AnimDone();
        animMenu.ResumeAnimDone();  
    }

    public void AnimationPartOfMenuDone()
    {
        animMenu.OptionShow();
    }

    //Termina la animación de Opcion Cerrar nos regresamos a PauseGame
    public void AnimationOptionDone()
    {
        animMenu.OptionDone();
    }
}
