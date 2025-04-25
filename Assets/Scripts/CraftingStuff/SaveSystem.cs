using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.Collections;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveData(GameManager gameManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Save.YALLSUCK";
        FileStream stream = new FileStream(path, FileMode.Create);
        SaveData data = new SaveData(EconomyManager.Instance,DayCycleManager.Instance);
        formatter.Serialize(stream, data);
    }
    public static SaveData LoadData()
    {
        string path = Application.persistentDataPath + "/Save.YALLSUCK";
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
            Debug.LogError("File Not Found in "+ path);
            return null;
        }
    }
}
