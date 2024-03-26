using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONDataService : IDataService
{
    public bool SaveData<T>(string RelativePath, T Data, bool Encrypted)
    {
        string path = Application.persistentDataPath + RelativePath;

        //If para hacer que si el file no existe se guarda, y si ya existe borra el viejo file y se guarda el nuevo
        try
        {
            //La linea 30 hace el proceso de pasar todo el texto a la ubicación donde se convertirá el texto a un archivo JSON
            if (File.Exists(path))
            {
                Debug.Log("Data exists. Deleting old file and writing a new one");
                File.Delete(path);
            }
            else
            {
                Debug.Log("Writing file for the first time!");
            }           
            using FileStream stream = File.Create(path);
            stream.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(Data));
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Unable to save data due to: {e.Message}{e.StackTrace}");
            return false;
        }
    }


    public T LoadData<T>(string RelativePath, bool Encrypted)
    {
        string path = Application.persistentDataPath + RelativePath;

        if(!File.Exists(path))
        {
            Debug.LogError($"Cannot load file at {path}.File does not exist!");
            throw new FileNotFoundException($"{path} does not exist");
        }

        try
        {
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load data due to: {e.Message} {e.StackTrace}");
            throw e;
        }
    }

    
}
