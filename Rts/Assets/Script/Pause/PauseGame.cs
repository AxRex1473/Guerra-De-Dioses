using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject _canvasPause;
    [SerializeField] private GameObject _canvasPartofPause;
    [SerializeField] private GameObject _canvasOptions;
    [SerializeField] private GameObject _canvasGame;
    [SerializeField] private Animator _pauseAnimator;
    [SerializeField] private Animator _bgAnimator;
    //[SerializeField] private Animator _partofPauseAnimator;
    [SerializeField] private Animator _optionAnim;
    //[SerializeField] private Animator _textAnim;
    //[SerializeField] private AudioSource _audioGame; //esto es para pausar el audio del juego
    //[SerializeField] private AudioSource _audioPause; //esto es para poner la m�sica de pausa.

    //Estos son para que funcione el guardado del juego
    private IDataService DataService = new JSONDataService();
    //Este es un placeholder, pero aqu� deber�a de tener una referencia de los datos generales del jugador ya sea su inventario la cantidad de tropas que lleva, etc.
    private StatConData _statConData = new StatConData();
    //private BuildingsData _buildingsData = new BuildingsData();
    private bool EncryptionEnabled;

  

    public void ToggleEncryption(bool EncryptionEnabled)
    {
        this.EncryptionEnabled = EncryptionEnabled;
    }  
  
   IEnumerator RestartLevel()
   {
        //Se restablece el tiempo porque de lo contrario no funciona el IEnumerator
        Time.timeScale = 1.0f;
        KILLALLNATIVES();
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }

    private void KILLALLNATIVES()
    {
        var objetivos = Object.FindObjectsOfType<Unit>();
        foreach (var unit in objetivos)
        {
            Destroy(unit.gameObject);
        }
    }

    public void SaveGame()
    {
        //_audioGame.Play();
        //audioPause.Pause();
        if (_statConData != null)
        {
            // Update StatConData with current stats
            _statConData.totalStone = StatCon.totalStone;
            _statConData.totalFood = StatCon.totalFood;
            _statConData.totalNative = StatCon.totalNative;
          


            if (DataService.SaveData("/player-Resources.json", _statConData, EncryptionEnabled))
            {
                Debug.Log("Game saved!");
            }
            else
            {
                Debug.LogError("Could not save file");
            }
        }
        if (LoadBuildings.buildingsData != null && LoadBuildings.buildingsData.Buildings.Count > 0)
        {
            if (DataService.SaveData("/player-Buildings.json", LoadBuildings.buildingsData, EncryptionEnabled))
            {
                Debug.Log("Game Salvado");
            }
            else
            {
                Debug.LogError("Could not save file");
            }
        }
        else
        {
            Debug.LogWarning("Building data list is empty or null. Cannot save buildings data.");
        }
    }


    //Aqu� se pone pausa a la m�sica del juego, se pone play a la m�sica de pausa y se pone el canvas
    public void Pause()
    {
        // _audioGame.Pause();
        //_audioPause.Play(); 
        _canvasPause.SetActive(true);
        _canvasGame.SetActive(false);
        _pauseAnimator.SetTrigger("MenuAction");
        _bgAnimator.SetTrigger("Fade");
        Time.timeScale = 0.0f;

    }

    public void Restart()
    {
        //_audioGame.Play();
        //audioPause.Pause();         
        //Esta funci�n vac�a la lista Singleton y despu�s reinicia el nivel despu�s de menos de un d�cimo de segundo.
        StartCoroutine(RestartLevel());

    }

    //La idea es que se reproduzca la animaci�n, despu�s se lanza un Event Animation en TestingAnim cu�ndo ese Evento sea triggereado AnimDone se ejecutar�.
    public void Resume()
    {
        _pauseAnimator.SetTrigger("MenuAction");
        _bgAnimator.SetTrigger("Fade");

    }

    //Aqu� se resume el juego y se desactiva el canvas
    public void AnimDone()
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
        _canvasPartofPause.SetActive(false);
        _canvasOptions.SetActive(true);
        _optionAnim.SetTrigger("NewOptions");
        //_pauseAnimator.SetTrigger("MenuAction");
    }

    public void BackToPauseCanvas()
    {
        //Se hace la animaci�n de ocultar el OptionAnim, pasamos al c�digo TestinAnim
        _optionAnim.SetTrigger("NewOptions");
        
    }

    public void OptionDone()
    {
        //Se desactiva el canvas Option, se activa la parte del canvas de Pausa y se hace la animaci�n de regreso del canvas de pausa.
        _canvasOptions.SetActive(false);
        _canvasPartofPause.SetActive(true);
        _pauseAnimator.SetTrigger("MenuOptions");
        //_partofPauseAnimator.SetTrigger("OptionsChange");
    }


}