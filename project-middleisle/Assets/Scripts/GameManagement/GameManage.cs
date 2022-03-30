using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManage : MonoBehaviour
{
    public enum GameState
    {
        SPLASH,
        MAINMENU,
        INTROCUTSCENE,
        INGAME
    }
    public GameState state;

    public GameObject splashScreen;
    public GameObject mainMenu;
    public GameObject loadMenu;
    public GameObject ingameUI;
    public GameObject transition;
    public GameObject loadButton;
    public GameObject dialoguePopUp;
    public GameObject IntroCutscene;

    public GameObject player;
    public List<string> pickedupObjects = new List<string>();
    public List<string> unlockedDoors = new List<string>(); // Tag the doors with the Door Tag. Found in the Inspector top left.

    //UI
    public Text notification;
    public AlertBacking alertBacking;
    public static GameManage gamemanager;

    public GameObject saveMenu;
    public Button[] saveSlots;
    public Button[] loadSlots;
    public int fileToSave = 0;
    public int fileToLoad = 0;

    //Settings
    public SettingsMenu settingsMenu;

    void Awake()
    {
        gamemanager = this;
    }

    private void Start()
    {
        state = GameState.SPLASH;

        int i = 0;

        foreach(Button save in loadSlots)
        {
            i++;
            string path = Application.persistentDataPath + "/gamedata" + i + ".fun";
            if (File.Exists(path))
            {
                save.gameObject.SetActive(true);
                GameData data = SaveSystem.LoadGame(i);
                save.GetComponentInChildren<Text>().text = data.dateTime;
                loadButton.SetActive(true);
                saveSlots[i-1].GetComponentInChildren<Text>().text = data.dateTime;
            }
            else
            {
                Debug.Log(path + " cannot be found.");
            }
        }
    }

    public void OnAnyKey(InputAction.CallbackContext context)
    {
        if (state == GameState.SPLASH)
        {
            if (context.performed)
            {
                StartCoroutine(StateTransition(GameState.MAINMENU, splashScreen, mainMenu));
                GetComponent<PlayerInput>().enabled = false;
            }
        }

        if (state == GameState.INTROCUTSCENE)
        {
            if (context.performed)
            {
                GetComponent<PlayerInput>().enabled = false;
                StartCoroutine(StartGameCoroutine());
                Debug.Log("Starting Game");
            }
        }
    }

    public void StartGame()
    {
        StartCoroutine(StateTransition(GameState.INTROCUTSCENE, mainMenu, IntroCutscene));
        GetComponent<PlayerInput>().enabled = true;
    }

    private IEnumerator StartGameCoroutine()
    {
        StartCoroutine(StateTransition(GameState.INGAME, IntroCutscene, null));
        player.SetActive(true);
        yield return null;
    }

    public void OpenSaveMenu()
    {
        saveMenu.SetActive(true);
    }

    public void ChangeSaveSlot(int i)
    {
        fileToSave = i;
    }

    public void SaveGame()
    {
        if (fileToSave == 0)
        {
            return;
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Door"))
        {
            if (!go.GetComponent<DoorController>().locked)
            {
                unlockedDoors.Add(go.name);
            }
        }

        saveSlots[fileToSave-1].GetComponentInChildren<Text>().text = SaveSystem.SaveGame(player.transform, fileToSave).dateTime;
    }

    public void ChangeLoadSlot(int i)
    {
        fileToLoad = i;
        LoadGame();
    }

    public void ToggleLoadMenu()
    {
        loadMenu.SetActive(!loadMenu.activeInHierarchy);
    }

    public void LoadGame()
    {
        if (fileToLoad == 0)
        {
            return;
        }

        StartCoroutine(StateTransition(GameState.INGAME, mainMenu, null));
        player.SetActive(true);

        GameData data = SaveSystem.LoadGame(fileToLoad);

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        player.transform.position = position;

        //Inventory load.
        Inventory.instance.items.Clear();

        foreach (string i in data.itemNames)
        {
            Debug.Log(i);
            Inventory.instance.Add((Item)Instantiate(Resources.Load("Items/" + i))); // Make sure to name your scriptable objects accordingly. Found in Resources/Items directory.
        }

        foreach (string j in data.inactiveItems)
        {
            GameObject.Find(j).SetActive(false);
            pickedupObjects.Add(j);
        }

        foreach (string k in data.unlockedDoors)
        {
            GameObject.Find(k).GetComponent<DoorController>().locked = false;
        }
    }

    public void OpenMenu()
    {
        if (dialoguePopUp.activeSelf == false)
        {
            ingameUI.SetActive(!ingameUI.activeSelf);
            AudioManager.Audio.Play("PauseGame");
            PlayerMove.character._direction = Vector3.zero;
//            if (ingameUI.activeSelf == true)
//            {
//                Pause.PauseInstance.PauseGame();
//            }
//            else
//            {
//                Pause.PauseInstance.Resume();
//            }
        }
    }

    // For the screen transitions. Be sure to change the timings in the future if using different transition animations.
    private IEnumerator StateTransition(GameState s, GameObject from, GameObject to)
    {
        transition.SetActive(true);
        yield return new WaitForSecondsRealtime(.5f);
        from.SetActive(false);
        if (to != null) to.SetActive(true);
        yield return new WaitForSecondsRealtime(.5f);
        transition.SetActive(false);
        state = s;
    }

    public void Interacting()
    {
        StopAllCoroutines();
        StartCoroutine(sendNotification("INTERACTING", 2));
    }

    public void TooFar()
    {
        StopAllCoroutines();
        StartCoroutine(sendNotification("\"I need to get closer.\"", 2));
    }

    public void Locked()
    {
        StopAllCoroutines();
        StartCoroutine(sendNotification("\"It won't open.\"", 2));
    }

    public void pickup(string itemName)
    {
        StopAllCoroutines();
        StartCoroutine(sendNotification("Picked up: " + itemName, 2));
    }

    IEnumerator sendNotification(string text, int time)
    {
        alertBacking.gameObject.SetActive(true);
        notification.text = text;
        alertBacking.Resize();
        yield return new WaitForSeconds(time);
        notification.text = "";
        alertBacking.gameObject.SetActive(false);

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AudioManager.Audio.Play("ExitGame");
    }

    public void GMQuitGame()
    {
        Application.Quit();
    }
}
