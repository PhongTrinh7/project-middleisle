using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine;

public class InventorySaver
{
    //To save inventory to file. Use static for quick access
    public static void SaveInventory()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Inventory.dat");
        Inventorysave toSaveInventory = new Inventorysave();

        toSaveInventory.items = Inventory.instance.items;

        bf.Serialize(file, toSaveInventory);
        file.Close();
    }

    //To load inventory from file. Use static modifier for quick access
    public static void LoadInventory()
    {
        if (File.Exists(Application.persistentDataPath + "/Inventory.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Inventory.dat", FileMode.Open);
            Inventory inventoryLoaded = (Inventory) bf.Deserialize(file);
            file.Close();

            Inventory.instance.items = inventoryLoaded.items;
        }
    }
}

[Serializable]
class Inventorysave
{
    //save a list of *item* type. this can be string, int and all type of data, See List<T> documentations C# or (Java if it helps). 
    public List<Item> items;
}