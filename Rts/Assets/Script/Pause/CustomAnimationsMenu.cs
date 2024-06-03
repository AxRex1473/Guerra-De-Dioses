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


}
