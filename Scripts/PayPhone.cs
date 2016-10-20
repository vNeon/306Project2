using UnityEngine;
using System.Collections;


/**
 * Implementation of the interaction with payphone
 **/ 
public class PayPhone : MonoBehaviour {

    public bool inTrigger;
    public bool isFound;

    public TextAsset message;
    public int fontSize;
    public Font font;
    public Texture2D backgroundTexture;

    public bool hasObjective;
    public string objective;

    public GameObject inventoryPanel;
    public GameObject[] inventoryIcons;
    public GameController gameController;
    public LevelTwoLogic logicController;
    public AudioClip pickUpAudio;
    public AudioClip hangUpAudio;

    private Rect label = new Rect(0, 60, 200, 25);

	// Use this for initialization
	void Start () {
	    
	}

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
    void Update () {
	    if (inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E)) //Interact with the payphone
            {
                isFound = true;
                inTrigger = false;
                logicController.triggerPayphone(); //trigger payphone logic (deduct money etc)
                AudioSource.PlayClipAtPoint(pickUpAudio, transform.position);
            }
        }

        if (isFound)
        {
            if (Input.GetKeyDown(KeyCode.F)) //Put down payphone
            {
                isFound = false;
                AudioSource.PlayClipAtPoint(hangUpAudio, transform.position);
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
        style.wordWrap = true;

        if (inTrigger && !GameController.getGameOver() && !GameController.getPausedStatus()) //Show gui indication for interacting with the payphone
        {
            GUI.Box(label, "Press E to Call Father");
        }
        else if (isFound && logicController.notEnoughMoney) //Pick up payphone but player doesnt have enough money
        {
            GUI.Box(new Rect(Screen.width - Screen.width / 2 - 250, Screen.height - Screen.height / 2 - 350, 500, 250), "Not enough money",style);
            GUI.Box(new Rect(0, 60, 200, 25), "Press F to exit");
        }
        else if (isFound && !logicController.notEnoughMoney) { //exit from payphone

            style.alignment = TextAnchor.UpperLeft;
            GUI.Box(new Rect(Screen.width - Screen.width / 2 - 250, Screen.height - Screen.height / 2 - 350, 500, 700), message.text, style);
            GUI.Box(new Rect(0, 60, 200, 25), "Press F to exit");
        }
    }
}
