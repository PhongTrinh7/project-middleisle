using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSettings
{
    public static void SaveOptions(SettingsMenu settings)
    {
        BinaryFormatter Settingsformatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/PersistantSettings.fun";
        FileStream Settingsstream = new FileStream(path, FileMode.Create);

        OptionsData data = new OptionsData(settings);

        Settingsformatter.Serialize(Settingsstream, data);
        Settingsstream.Close();
    }

    public static OptionsData LoadSettings()
    {
        string path = Application.persistentDataPath + "/PersistantSettings.fun";
        if (File.Exists(path))
        {
            BinaryFormatter Settingsformatter = new BinaryFormatter();
            FileStream Settingsstream = new FileStream(path, FileMode.Open);
            OptionsData data = Settingsformatter.Deserialize(Settingsstream) as OptionsData;

            Settingsstream.Close();


            return data;

        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
