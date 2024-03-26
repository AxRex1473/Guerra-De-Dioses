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

    private PlayerStats PlayerStats = new PlayerStats();
    private IDataService DataService=new JSONDataService();
    private bool EncryptionEnabled;
    private long SaveTime;


    public void ToggleEncryption(bool EncryptionEnabled)
    {
        this.EncryptionEnabled = EncryptionEnabled;
    }

    public void SerializeJSON()
    {
        long startTime = DateTime.Now.Ticks;
        if(DataService.SaveData("/player-stats.json",PlayerStats,EncryptionEnabled))
        {
            SaveTime = DateTime.Now.Ticks - startTime;
            SaveTimeText.SetText($"Save Time:{(SaveTime / 10000f):N4}ms");
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
