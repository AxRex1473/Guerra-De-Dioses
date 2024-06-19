using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Para que una animaci�n nueva funcione tiene que cumplir varias cosas.
//1- Animator con Update Mode Unscaled Time para que funcione a�nque este pausado el juego y animaciones que no esten en Loop
//2- Un estado "Idle" donde empiece el Animator pero con una animaci�n en la que no haga nada
// Esto es solo para que pueda hacer la transici�n a la animaci�n inicial con el trigger designado.
//3- M�nimo 3 funciones en este c�digo y 1 en TestingAnim: Una para activar el objeto y hacer la animaci�n inicial
//Otra funci�n para �nicamente reproducir la segunda animaci�n del objeto con el Trigger del Animator
//Despu�s la funci�n en TestingAnim que solo se ejecutar� por un evento al final de la segunda animaci�n
//Dicha funci�n en TestingAnim llamar� a la tercera funci�n de este c�digo que solo apagar� el objeto
//y en algunos casos prender� otro objeto.
//EJ: Pausa- El Bot�n dentro del GameCanvases tiene un Obj llamado PauseButton este tiene 2 funciones 
//al darle click, PauseGame.Pause (L�gica de Parar el tiempo), y CustomAnimations...PauseAnim (Se encarga
//de la l�gica para activar el objeto del Pause Canvas, desactivar el Canvas del juego y reproudcir la 
//animaci�n del PauseCanvas y tambi�n del Fade del Texto.
//Y de ah� si quiere resumir el juego el Resume Button tiene la funci�n Resume el cu�l da la animaci�n
//del men� de Pausa para cerrar el juego, de ah� en TestingAnim tiene la funci�n AnimationDoDone 
//que espera a que termine la animaci�n para llamar la �ltima funci�n del CustomAnimations que ser�a 
//ResumeAnimDone �l cu�l desactiva el Objeto del PauseCanvas y activa el Canvas del Juego.

public class CustomAnimationsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _canvasPause,_canvasOptions,_canvasGame,_popUpObject,_popUpRestartLV;
    [SerializeField] private Animator _pauseAnimator, _bgAnimator, _optionAnim,_textOptionAnim,_popUpAnimator,_popUpRestartAnim;

    public void PauseAnim()
    {
        _canvasPause.SetActive(true);
        _canvasGame.SetActive(false);
        _pauseAnimator.SetTrigger("MenuAction");
        _bgAnimator.SetTrigger("Fade");
    }

    //La idea es que se reproduzca la animaci�n, despu�s se lanza un Event Animation en TestingAnim cu�ndo ese Evento sea triggereado AnimDone se ejecutar�.
    public void Resume()
    {
        _pauseAnimator.SetTrigger("MenuAction");
        _bgAnimator.SetTrigger("Fade");

    }

    //Se resume el juego y se oculta el Pause Canvas
    public void ResumeAnimDone()
    {
        _canvasPause.SetActive(false);
        _canvasGame.SetActive(true);
        Time.timeScale = 1.0f;
    }

    //Hace la otra animaci�n del men� de pausa para que se cierre y as� en Testing Anim de la se�al para hacer el OptionShow
    public void OptionBtn()
    {
        _pauseAnimator.SetTrigger("MenuOptions");
    }

    public void OptionShow()
    {
        //Aqu� hace animaci�n de ocultar la parte del PauseCanvas y despu�s la animaci�n que muestra las opciones

        _canvasOptions.SetActive(true);
        // _canvasPartofPause.SetActive(false);
        _textOptionAnim.SetTrigger("Texting");
        _optionAnim.SetTrigger("NewOptions");

        //_pauseAnimator.SetTrigger("MenuAction");
    }

    public void BackToPauseCanvas()
    {
        //Se hace la animaci�n de ocultar el OptionAnim, pasamos al c�digo TestinAnim
        _textOptionAnim.SetTrigger("Texting");
        _optionAnim.SetTrigger("NewOptions");

    }

    public void OptionDone()
    {
        //Se desactiva el canvas Option, se activa la parte del canvas de Pausa y se hace la animaci�n de regreso del canvas de pausa.
        _canvasOptions.SetActive(false);
        _pauseAnimator.SetTrigger("MenuOptions");
        //_partofPauseAnimator.SetTrigger("OptionsChange");
    }

    //Hace la animaci�n del men� de pausa para cerrar el juego, y as� despu� darle la 
    public void AreYouSureBtn()
    {
        _popUpObject.SetActive(true);
        _popUpAnimator.SetTrigger("PopUpSave");
    }

    //Regresa al men� de Pausa despu�s de darle al boton de no en el PopUp de Opciones, de ah� el c�digo pasa a TestingAnim
    public void NotSure()
    {
        _popUpAnimator.SetTrigger("PopUpSave");
    }

    //Funci�n para el TestingAnim despu�s de terminar la animaci�n de PopUp
    public void PopUpExitDone()
    {
        _popUpObject.SetActive(false);
    }

    public void SureRestartBtn()
    {
        _popUpRestartLV.SetActive(true);
        _popUpRestartAnim.SetTrigger("RestartPopUp");
    }

    public void NotSureRestart()
    {
        _popUpRestartAnim.SetTrigger("RestartPopUp");
    }

    public void RestartPopUpDone()
    {
        _popUpRestartLV.SetActive(false);
    }


}
