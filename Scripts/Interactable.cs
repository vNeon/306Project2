using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/* This script controls the behaviour of objects which are collected
 * by the player.
 * When an object is picked up the player will recieve text associated 
 * with the object.
 */
public class Interactable : MonoBehaviour {
    // public variables which can be changed within unity.
    public bool inTrigger;
    public bool isFound;

    public TextAsset message;
    public int fontSize;        
    public Font font;
    public Texture2D backgroundTexture;

    public bool hasObjective;
    public string objective;
    public bool isFinalObject;

    public int itemScore;
	public GameObject inventoryPanel;
	public GameObject[] inventoryIcons;
	public GameController gameController;
    public SpawnItems spawner;
	private Rect label = new Rect (0, 60, 200, 25);

    public ObjectPickupSound soundEffect;
	private static bool levelComplete;
    
    // Use this for initialization.
    void Start()
    {
		levelComplete = false;
        inTrigger = false;
        isFound = false;
	}
    
    // Check if the player enters the objects collider.
    void OnTriggerEnter(Collider other)
    {
		if (other.tag == "Player") {
			inTrigger = true;
		}
    }

    // Check if the player enters the objects collider.
    void OnTriggerExit(Collider other)
    {
		if (other.tag == "Player") {
			inTrigger = false;
		}
    }

    // Returns whether the level has ended.
	public static bool getLevelStatus(){
		return levelComplete;
	}
	// Returns whether the level has ended.
	public static void setLevelStatus(bool status){
		levelComplete = status;
	}
    // Update is called once per frame.
    void Update()
	{
		if (inTrigger)
        {
			if (Input.GetKeyDown (KeyCode.E))
            {
				isFound = true;
				inTrigger = false;

                // Makes object invisible.
				gameObject.
                    GetComponent<BoxCollider> ().enabled = false;
				gameObject.GetComponent<Renderer> ().enabled = false;
                
                //Play pick up sound 
				if (soundEffect != null) {
					soundEffect.playEffect();
				}

                // Adds to player score.
				gameController.AddScore (itemScore);
				gameController.sleepMeter.value += itemScore / 2;

                // Adds object to inventory.
				if (inventoryIcons.Length != 0) {
					GameObject i;
					i = Instantiate (inventoryIcons [0]);
					i.transform.SetParent (inventoryPanel.transform);
				}

                // Updates game objective.
                if(hasObjective)
                {
                    Text tmp = gameController.objectiveText.GetComponent<Text>();
                    tmp.text = objective;
                }
			}
		}
        // Once player reads the text destroy object.
		if (isFound) {
			if (Input.GetKeyDown (KeyCode.F)) {
                if (isFinalObject){
					levelComplete = true;
				}
                if (this.gameObject.tag.Equals("spawnFixed"))
                {
                    spawner.SpawnNewItemFixed();
                }
                else if (this.gameObject.tag.Equals("spawnRand"))
                {
                    spawner.SpawnNewItemRandom();
                }
				Destroy (this.gameObject);
			}
		}
	}

    // Displays text to player telling them how to pick up the object
    // and exit from text
    void OnGUI()
	{
		if (inTrigger && !GameController.getGameOver() && !GameController.getPausedStatus()) {
			GUI.Box (label, "Press E to interact");
		} else if (isFound) {

            //Gui Style
            GUIStyle style = new GUIStyle("box");
            style.fontSize = fontSize;
            style.font = font;
            style.normal.textColor = Color.black;
            style.normal.background = backgroundTexture;

            GUI.Box (new Rect (Screen.width - Screen.width / 2 - 250, Screen.height - Screen.height / 2 - 350, 500, 700), message.text, style);
			GUI.Box (new Rect (0, 60, 200, 25), "Press F to exit");
		}
	}
}
