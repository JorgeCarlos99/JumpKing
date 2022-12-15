using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System;
public class SaveManager : MonoBehaviour
{
    public SaveData activeSave;
    public static SaveManager instance;
    public bool hasLoaded;


    private void Awake()
    {
        instance = this;
        Load();
    }

    public void Save()
    {
        string dataPath = Application.persistentDataPath;
        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Create);
        serializer.Serialize(stream, activeSave);        
        Debug.Log("Saved");
        stream.Close();
    }

    public void Load()
    {
        string dataPath = Application.persistentDataPath;

        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Open);
            activeSave = serializer.Deserialize(stream) as SaveData;

            stream.Close();
            hasLoaded = true;
            Debug.Log("Loaded Data");
        }
    }

    public void DeleteSavedData()
    {
        try
        {
            string dataPath = Application.persistentDataPath;
            if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
            {
                File.Delete(dataPath + "/" + activeSave.saveName + ".save");
                Debug.Log("Data delete" + dataPath + "/" + activeSave.saveName + ".save");
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e, this);
        }
    }
}

[System.Serializable]
public class SaveData
{
    public string saveName;
    public int spears;
    public Vector3 position;
    public string lanza1;
    public string lanza2;

}