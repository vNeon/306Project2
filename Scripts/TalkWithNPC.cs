using UnityEngine;
using System.Collections;

//Author: Jack Wong
//Player could interact with npc by pressing F button.
//attach to the NPC that could be interacted with player

public class TalkWithNPC : MonoBehaviour {

	public GameController mainController;
	public LevelTwoLogic logicController;
	public bool isStop = false;
	public bool isTeacher;
	public bool isMultipleText = false;

	public int TeacherScore;


	public TextAsset[] dialogue;

	public int fontSize;
	public Font font;
	public Texture2D backgroundTexture;

	private bool showDialogue;
	private GameObject player;
	private int counter;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		isStop = false;
		showDialogue = false;
		counter = 0;
	}

	// Update is called once per frame
	void Update () {
		//Check if npc is stop
		if (Vector3.Distance (player.transform.position, this.gameObject.transform.position) < 2) {
			isStop = true;
		} else {
			isStop = false;
		}
		if (logicController != null) {
			if (logicController.hasBoughtWater) {
				counter = 1;
			}
		}

		if (isStop ) {
			if (!isMultipleText) {
				if (!showDialogue && Input.GetKeyDown (KeyCode.F)) {

                    //Logic for level 2 puzzles
					if (isTeacher && !logicController.hasBoughtWater) { //Talking to teacher to begin "quest" 
						logicController.startQuest (); 
					} else if (isTeacher && logicController.hasBoughtWater) { //Giving water to teacher
						if (!PlayerPrefs.HasKey ("GoodStudent")) { 
							mainController.UnlockAchievement ("GoodStudent", "Goody:Bought water for teacher.");
						}
						mainController.AddScore (TeacherScore);
                        mainController.objectiveText.text = "There's a payphone somewhere I can use to call dad";
					}
					Talk ();
				} else if (showDialogue && Input.GetKeyDown (KeyCode.F)) {
					showDialogue = false;

				}
			} else {
                // Manages the format of the display text 
				if (Input.GetKeyDown (KeyCode.F) && counter < dialogue.Length) {
					showDialogue = true;
					counter += 1;
				} else if (Input.GetKeyDown(KeyCode.F) && counter >= dialogue.Length){
					showDialogue = false;
					counter = 0;
				}
			}

		}
	}
	void Talk(){

		showDialogue = true;
	}

	//Show the dialogue box
	void OnGUI(){
		if (isStop && !GameController.getGameOver() && !GameController.getPausedStatus()) { //check if player is within range of npc and game is not over/paused
			GUIStyle style = new GUIStyle("box");
			style.fontSize = fontSize;
			style.font = font;
			style.normal.textColor = Color.black;
			style.normal.background = backgroundTexture;
			style.alignment = TextAnchor.UpperLeft;
			style.wordWrap = true;

			if (showDialogue) { //If dialogue is being shown, show box to close dialogue
				GUI.Box(new Rect(Screen.width - Screen.width / 2 - 250, Screen.height - Screen.height / 2 - 125, 500, 250), dialogue[counter].text, style);
				GUI.Box(new Rect(0, 60, 200, 25), "Press F to exit");

			} else { //If dialogue is not shown, show guide to open dialogue
				GUI.Box (new Rect (0, 60, 200, 25), "Press F to interact");
			}
		}

	}

}

	
