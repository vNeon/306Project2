using UnityEngine;
using System.Collections;

/**
 * Implementation of the interation with the vending machine in level 2
 **/ 
public class VendingMachine : MonoBehaviour {

    public bool inTrigger;
    public bool isFound;

    public TextAsset message;
    public int fontSize;
    public Font font;
    public Texture2D backgroundTexture;

    public bool hasObjective;
    public string objective;
    public int Score;

    public GameObject inventoryPanel;
    public GameObject[] inventoryIcons;
    public GameController gameController;
    public LevelTwoLogic logicController;

    public AudioClip vendingAudio;

    private Rect label = new Rect(0, 60, 200, 25);

    // Check if the player enters the objects collider.
    void OnTriggerEnter(Collider other)
    {
        inTrigger = true;
    }

    // Check if the player enters the objects collider.
    void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Check for user interaction
        if (inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                isFound = true;
                inTrigger = false;
                if (logicController.spendMoney()) { //Checks if player has enough money to buy water

                    // Adds to player score.
                    gameController.AddScore(Score);
                    gameController.sleepMeter.value += Score / 2;

                    AudioSource.PlayClipAtPoint(vendingAudio, transform.position);
                }

            }
        }

        if (isFound)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isFound = false;
            }
        }
    }

    // Displays text to player telling them how to pick up the object
    // and exit from text
    void OnGUI()
    {
        GUIStyle style = new GUIStyle("box");
        style.fontSize = fontSize;
        style.font = font;
        style.normal.textColor = Color.black;
        style.normal.background = backgroundTexture;
    
        if (inTrigger && !GameController.getGameOver() && !GameController.getPausedStatus())
        {
            GUI.Box(label, "Press E to Buy Water");
        }
        else if (isFound && logicController.notEnoughMoney) //Not enough money to buy water
        { 
            GUI.Box(new Rect(Screen.width - Screen.width / 2 - 250, Screen.height - Screen.height / 2 - 50, 500, 100), "Not enough money", style);
            GUI.Box(new Rect(0, 60, 200, 25), "Press F to exit");
        }
        else if (isFound && !logicController.notEnoughMoney) //gui indication to exit from interaction screen
        {
            GUI.Box(new Rect(Screen.width - Screen.width / 2 - 250, Screen.height - Screen.height / 2 - 50, 500, 100), message.text, style);
            GUI.Box(new Rect(0, 60, 200, 25), "Press F to exit");
        }
    }
}
