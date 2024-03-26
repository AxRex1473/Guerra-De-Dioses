using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    //Estos son para que funcione el guardado del juego
    private IDataService DataService = new JSONDataService();
    //Este es un placeholder, pero aquí debería de tener una referencia de los datos generales del jugador ya sea su inventario la cantidad de tropas que lleva, etc.
    private PlayerStats PlayerStats = new PlayerStats();
    private bool EncryptionEnabled;

    public void ToggleEncryption(bool EncryptionEnabled)
    {
        this.EncryptionEnabled = EncryptionEnabled;
    }


    LoadGame()
    {
        PlayerStats data = DataService.LoadData<PlayerStats>("/player-stats.json", EncryptionEnabled);
    }
}
