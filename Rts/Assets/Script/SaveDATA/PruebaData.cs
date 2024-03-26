using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Newtonsoft.Json;

public class PruebaData : MonoBehaviour
{
    //Texto de prueba para ver que se guarde
    [SerializeField] private TextMeshProUGUI SourceDataText;    


    //Esto es para ver cuanto tarda en guardar
    [SerializeField] private TMP_InputField InputField;
    [SerializeField] private TextMeshProUGUI SaveTimeText;
    [SerializeField] private TextMeshProUGUI LoadTimeText;

    private PlayerStats PlayerStats = new PlayerStats();
    private IDataService DataService=new JSONDataService();
    private bool EncryptionEnabled;
    private long SaveTime;
    private long LoadTime;

    public void ToggleEncryption(bool EncryptionEnabled)
    {
        this.EncryptionEnabled = EncryptionEnabled;
    }

    public void SerializeJSON()
    {
        //Checa cuanto tarda en guardar el archivo
        long startTime = DateTime.Now.Ticks;
        //Linea de ejemplo para guardar data.
        if(DataService.SaveData("/player-stats.json",PlayerStats,EncryptionEnabled))
        {
            SaveTime = DateTime.Now.Ticks - startTime;
            SaveTimeText.SetText($"Save Time:{(SaveTime / 10000f):N4}ms");
            startTime = DateTime.Now.Ticks;
            try
            {
                //Carga el archivo, ahorita el Encryption no hace nada. Sirve para visualizar cuanto tarda en cargar el archivo
                PlayerStats data=DataService.LoadData<PlayerStats>("/player-stats.json",EncryptionEnabled);
                LoadTime = DateTime.Now.Ticks - startTime;
                InputField.text = "Loaded from file:\r\n" + JsonConvert.SerializeObject(data, Formatting.Indented);
                LoadTimeText.SetText($"Load Time: {(LoadTime / TimeSpan.TicksPerMillisecond):N4}ms");
            }
            catch (Exception e)
            {
                Debug.LogError($"Could not read file! Show something on the UI here!");
                InputField.text = "<color=#ff0000>Error reading save file!</color>";
            }
        }
        else
        {
            Debug.LogError("Could not save file");
            InputField.text = "<color=#ff000>Error saving data!</color>";
        }
    }

    private void Awake()
    {
        SourceDataText.SetText(JsonConvert.SerializeObject(PlayerStats, Formatting.Indented));
    }
}
