using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class GameManage : MonoBehaviour
{
    public enum GameState
    {
        SPLASH,
        MAINMENU,
        INGAME
    }
    public GameState state;

    public GameObject splashScreen;
    public GameObject mainMenu;
    public GameObject ingameUI;
    public GameObject transition;

    public bool loadable;
    public GameObject loadButton;

    public GameObject player;

    //UI
    public Text notification;
    public AlertBacking alertBacking;
    public static GameManage gamemanager;

    void Awake()
    {
        gamemanager = this;
    }

    private void Start()
    {
        state = GameState.SPLASH;

        if (loadable)
        {
            loadButton.SetActive(true);
        }
    }

    private void Update()
    {
        
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
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        StartCoroutine(StateTransition(GameState.INGAME, mainMenu, ingameUI));
        player.SetActive(true);
        yield return null;
    }

    public void LoadGame()
    {
        StartCoroutine(StateTransition(GameState.INGAME, mainMenu, ingameUI));
        player.SetActive(true);
        player.GetComponent<PlayerMove>().LoadPlayer();
    }

    public void Options()
    {
        //TODO
        Debug.Log("Options");
    }

    // For the screen transitions. Be sure to change the timings in the future if using different transition animations.
    private IEnumerator StateTransition(GameState s, GameObject from, GameObject to)
    {
        transition.SetActive(true);
        yield return new WaitForSecondsRealtime(.5f);
        from.SetActive(false);
        to.SetActive(true);
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

    //TESTING LOAD
    public void LoadPlayerTest()
    {
        if (loadable) player.GetComponent<PlayerMove>().LoadPlayer();
    }
}
