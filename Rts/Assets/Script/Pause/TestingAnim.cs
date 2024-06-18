using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestingAnim : MonoBehaviour
{
    //Este código sirve para todos los eventos al terminar una animación.
    [SerializeField] private CustomAnimationsMenu animMenu;

    //Una vez que termine la animación se ejecuta la Función AnimDone() en el PauseGame.
    public void AnimationDoDone()
    {

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

    //Termina la animación del PopUP para regresarnos al PauseGame
    public void AnimationPopUpDone()
    {
        animMenu.PopUpExitDone();
    }


    public void RestartPopUpDone()
    {
        animMenu.RestartPopUpDone();
    }
}
