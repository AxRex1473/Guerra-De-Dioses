using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject _canvasPause;
    [SerializeField] private GameObject _canvasGame;
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


    //Aqu� se pone pausa a la m�sica del juego, se pone play a la m�sica de pausa y se pone el canvas
    public void Pause()
    {
        // _audioGame.Pause();
        //_audioPause.Play(); 
        _canvasPause.SetActive(true);
        _canvasGame.SetActive(false);
        Time.timeScale = 0.0f;

    }

    //Aqu� se resume el juego y se desactiva el canvas
    public void Resume()
    {
        //_audioGame.Play();
        //audioPause.Pause();
        _canvasPause.SetActive(false);
        _canvasGame.SetActive(true);
        Time.timeScale = 1.0f;
    }

    public void Restart()
    {
        //_audioGame.Play();
        //audioPause.Pause();
        //Limpiar la lista no har� nada, es necesario borrar a los monos para que funcione O cambiar la lista Singleton
        //Pero se necesitar�a de una implementaci�n m�s profunda para que funcione.
        //UnitSelections.Instance.UnitList.Clear();              
        //StartCoroutine(RestartLevel());
        KillThemAll();
        
    }

    public void KillThemAll()
    {
        KILLALLNATIVES();
        bool areNativesGone = AreAllNativesGone();
        if (areNativesGone)
        {
            // Load the scene again
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1.0f;
        }
    }

    private void KILLALLNATIVES()
    {
        var objetivos = Object.FindObjectsOfType<Unit>();
        foreach (var unit in objetivos)
        {
            Destroy(unit.gameObject);
        }
    }

    private bool AreAllNativesGone()
    {
        var units = Object.FindObjectsOfType<Unit>();
        return units.Length == 0;
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

   

    /*
    IEnumerator RestartLevel()
    {
        KILLALLNATIVES();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;

    }*/
}