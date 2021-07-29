using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine;

public class SaveInventory
{
    public List<Item> inventory = new List<Item>();

    //To save inventory to file. Use static for quick access
    private void save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Inventory.dat");
        Inventory toSaveInventory = new Inventory();

        toSaveInventory.inventory = this.inventory;

        bf.Serialize(file, inventory);
        file.Close();
    }

    //To load inventory from file. Use static modifier for quick access
    private void load()
    {
        if (File.Exists(Application.persistentDataPath + "/Inventory.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Inventory.dat", FileMode.Open);
            Inventory InventoryLoaded = (Inventory)bf.Deserialize(file);
            file.Close();

            this.inventory = InventoryLoaded.inventory;
        }//if

    }//load function

    //To load inventory from file with returns. Use static modifier for quick access
    private List<Item> load()
    {
        if (File.Exists(Application.persistentDataPath + "/Inventory.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Inventory.dat", FileMode.Open);
            Inventory InventoryLoaded = (Inventory)bf.Deserialize(file);
            file.Close();

            return InventoryLoaded.inventory;
        }//if
        return new List<Item>();
    }//load function

}//class

[Serializable]
class Inventory
{
    //save a list of *item* type. this can be string, int and all type of data, See List<T> documentations C# or (Java if it helps). 
    public List<Item> inventory;
}