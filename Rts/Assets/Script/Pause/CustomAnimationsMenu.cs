using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAnimationsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _canvasPause;
    [SerializeField] private GameObject _canvasOptions;
    [SerializeField] private GameObject _canvasGame;
    [SerializeField] private Animator _pauseAnimator;
    [SerializeField] private Animator _bgAnimator;
    //[SerializeField] private Animator _partofPauseAnimator;
    [SerializeField] private Animator _optionAnim;
    [SerializeField] private Animator _textOptionAnim;
    

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


}
