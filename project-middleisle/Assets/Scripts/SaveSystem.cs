using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    public static void SavePlayer(PlayerMove playerMove)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/playermove.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(playerMove);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/playermove.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close();

            return data;

        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveInventory()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream file = File.Create(Application.persistentDataPath + "/inventory.fun");
        InventoryData inventorySave = new InventoryData(Inventory.instance);

        formatter.Serialize(file, inventorySave);
        file.Close();
    }

    //To load inventory from file. Use static modifier for quick access
    public static InventoryData LoadInventory()
    {
        string path = Application.persistentDataPath + "/inventory.fun";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            InventoryData inventoryLoaded = (InventoryData)formatter.Deserialize(file);
            file.Close();

            return inventoryLoaded;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}

[System.Serializable]
public class GameData
{
    public bool loadable;

    public float[] position;
    public List<string> itemNames;
    public List<string> inactiveItems;


    public GameData(List<string> inactiveItems, PlayerMove playerMove, Inventory playerInventory)
    {
        loadable = true;

        position = new float[3];
        position[0] = playerMove.transform.position.x;
        position[1] = playerMove.transform.position.y;
        position[2] = playerMove.transform.position.z;

        itemNames = new List<string>();

        foreach (Item i in playerInventory.items)
        {
            itemNames.Add(i.name);
        }
    }
}

[System.Serializable]
public class InventoryData
{
    public List<string> itemNames;

    public InventoryData(Inventory playerInventory)
    {
        itemNames = new List<string>();

        foreach (Item i in playerInventory.items)
        {
            itemNames.Add(i.name);
        }
    }
}
