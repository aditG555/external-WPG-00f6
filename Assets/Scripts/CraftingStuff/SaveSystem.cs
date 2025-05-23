using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.Collections;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveData(GameManager gameManager, EconomyManager economyManager, DayCycleManager dayCycleManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Save.DATSAVE";
        FileStream stream = new FileStream(path, FileMode.Create);
        SaveData data = new SaveData(economyManager, dayCycleManager, gameManager);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static SaveData LoadData()
    {
        string path = Application.persistentDataPath + "/Save.DATSAVE";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);
            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Saves File Not Found in "+ path);

            return null;
        }
    }
}
