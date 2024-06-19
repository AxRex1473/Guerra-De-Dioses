using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
 
    //[SerializeField] private AudioSource _audioGame; //esto es para pausar el audio del juego
    //[SerializeField] private AudioSource _audioPause; //esto es para poner la m�sica de pausa.

    //Estos son para que funcione el guardado del juego
    private IDataService DataService = new JSONDataService();
    //Este es un placeholder, pero aqu� deber�a de tener una referencia de los datos generales del jugador ya sea su inventario la cantidad de tropas que lleva, etc.
    private StatConData _statConData = new StatConData();
    private bool EncryptionEnabled;
    [SerializeField] private CustomAnimationsMenu _customRef;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("A�a");
            Pause();
            _customRef.PauseAnim();
        }
    }

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


    //Aqu� se pone pausa a la m�sica del juego, se pone play a la m�sica de pausa y el canvas se pone en CustomAnimationsMenu
    public void Pause()
    {
        // _audioGame.Pause();
        //_audioPause.Play(); 

        Time.timeScale = 0.0f;

    }

    public void Restart()
    {
        //_audioGame.Play();
        //audioPause.Pause();                
        StartCoroutine(RestartLevel());
     
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}