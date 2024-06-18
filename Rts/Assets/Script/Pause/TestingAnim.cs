using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestingAnim : MonoBehaviour
{
    //Este c�digo sirve para todos los eventos al terminar una animaci�n.
    [SerializeField] private CustomAnimationsMenu animMenu;

    //Una vez que termine la animaci�n se ejecuta la Funci�n AnimDone() en el PauseGame.
    public void AnimationDoDone()
    {

        animMenu.ResumeAnimDone();  
    }

    public void AnimationPartOfMenuDone()
    {
        animMenu.OptionShow();
    }

    //Termina la animaci�n de Opcion Cerrar nos regresamos a PauseGame
    public void AnimationOptionDone()
    {
        animMenu.OptionDone();
    }

    //Termina la animaci�n del PopUP para regresarnos al PauseGame
    public void AnimationPopUpDone()
    {
        animMenu.PopUpExitDone();
    }


    public void RestartPopUpDone()
    {
        animMenu.RestartPopUpDone();
    }
}
