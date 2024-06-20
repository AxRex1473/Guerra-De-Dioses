using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Para que una animación nueva funcione tiene que cumplir varias cosas.
//1- Animator con Update Mode Unscaled Time para que funcione aúnque este pausado el juego y animaciones que no esten en Loop
//2- Un estado "Idle" donde empiece el Animator pero con una animación en la que no haga nada
// Esto es solo para que pueda hacer la transición a la animación inicial con el trigger designado.
//3- Mínimo 3 funciones en este código y 1 en TestingAnim: Una para activar el objeto y hacer la animación inicial
//Otra función para únicamente reproducir la segunda animación del objeto con el Trigger del Animator
//Después la función en TestingAnim que solo se ejecutará por un evento al final de la segunda animación
//Dicha función en TestingAnim llamará a la tercera función de este código que solo apagará el objeto
//y en algunos casos prenderá otro objeto.
//EJ: Pausa- El Botón dentro del GameCanvases tiene un Obj llamado PauseButton este tiene 2 funciones 
//al darle click, PauseGame.Pause (Lógica de Parar el tiempo), y CustomAnimations...PauseAnim (Se encarga
//de la lógica para activar el objeto del Pause Canvas, desactivar el Canvas del juego y reproudcir la 
//animación del PauseCanvas y también del Fade del Texto.
//Y de ahí si quiere resumir el juego el Resume Button tiene la función Resume el cuál da la animación
//del menú de Pausa para cerrar el juego, de ahí en TestingAnim tiene la función AnimationDoDone 
//que espera a que termine la animación para llamar la última función del CustomAnimations que sería 
//ResumeAnimDone él cuál desactiva el Objeto del PauseCanvas y activa el Canvas del Juego.

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

    //La idea es que se reproduzca la animación, después se lanza un Event Animation en TestingAnim cuándo ese Evento sea triggereado AnimDone se ejecutará.
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

    //Hace la otra animación del menú de pausa para que se cierre y así en Testing Anim de la señal para hacer el OptionShow
    public void OptionBtn()
    {
        _pauseAnimator.SetTrigger("MenuOptions");
    }

    public void OptionShow()
    {
        //Aquí hace animación de ocultar la parte del PauseCanvas y después la animación que muestra las opciones

        _canvasOptions.SetActive(true);
        // _canvasPartofPause.SetActive(false);
        _textOptionAnim.SetTrigger("Texting");
        _optionAnim.SetTrigger("NewOptions");

        //_pauseAnimator.SetTrigger("MenuAction");
    }

    public void BackToPauseCanvas()
    {
        //Se hace la animación de ocultar el OptionAnim, pasamos al código TestinAnim
        _textOptionAnim.SetTrigger("Texting");
        _optionAnim.SetTrigger("NewOptions");

    }

    public void OptionDone()
    {
        //Se desactiva el canvas Option, se activa la parte del canvas de Pausa y se hace la animación de regreso del canvas de pausa.
        _canvasOptions.SetActive(false);
        _pauseAnimator.SetTrigger("MenuOptions");
        //_partofPauseAnimator.SetTrigger("OptionsChange");
    }

    //Hace la animación del menú de pausa para cerrar el juego, y así despué darle la 
    public void AreYouSureBtn()
    {
        _popUpObject.SetActive(true);
        _popUpAnimator.SetTrigger("PopUpSave");
    }

    //Regresa al menú de Pausa después de darle al boton de no en el PopUp de Opciones, de ahí el código pasa a TestingAnim
    public void NotSure()
    {
        _popUpAnimator.SetTrigger("PopUpSave");
    }

    //Función para el TestingAnim después de terminar la animación de PopUp
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
