using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    //Estos son para que funcione el guardado del juego
    private IDataService DataService = new JSONDataService();
    private bool EncryptionEnabled;

    public void ToggleEncryption(bool EncryptionEnabled)
    {
        this.EncryptionEnabled = EncryptionEnabled;
    }

    public void LoadGameFile()
    {        
        try
        {
            //Carga el archivo, ahorita el Encryption no hace nada. Sirve para visualizar cuanto tarda en cargar el archivo
            StatCon data = DataService.LoadData<StatCon>("/player-stats.json", EncryptionEnabled);
        }
        catch (Exception e)
        {
            Debug.LogError($"Could not read file! Show something on the UI here!");
        }
    }

}
