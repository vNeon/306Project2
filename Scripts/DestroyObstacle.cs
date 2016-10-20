using UnityEngine;
using System.Collections;

//Author: Jack Wong
//To destroy the obstacle when the axe is picked
//attach this script to obstacle that needs to be destroyed.
public class DestroyObstacle : MonoBehaviour {
	private bool hasAxe;
	public GameObject gamecontroller;
    public AudioClip destroyAudio;

	private GameObject player;
	private bool inTrigger;
	// Use this for initialization
	void Start () {
		hasAxe = false;
		inTrigger = false;
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update(){
		CheckPlayerInventory ();

        // When the player is in range of the tree and presses down E, if the player has an axe destroy the tree
		if (inTrigger && Input.GetKeyDown(KeyCode.E)) {
			
            
			if (hasAxe) {
				Destroy (this.gameObject);
                AudioSource.PlayClipAtPoint(destroyAudio, transform.position);

                //unlocked lumber jack achievement on first time cutting a tree
                if (!PlayerPrefs.HasKey("LumberJack"))
                {
                    gamecontroller.GetComponent<GameController>().UnlockAchievement("LumberJack", "Lumber Jack: Chopped first tree");
                }
			}
		}
	}
	//Check if the obstacle is entered by player
	void OnTriggerEnter(Collider collider){
		if (collider.tag == "Player") {
			inTrigger = true;
		}

	}
	void OnTriggerExit(Collider collider){
		if (collider.tag == "Player") {
			inTrigger = false;
		}
	}
	//check the gamecontroller for axe
	void CheckPlayerInventory(){
		hasAxe = gamecontroller.GetComponent<GameController> ().getAxe ();
	}
	//display instruction
	void OnGUI(){
		if (inTrigger && hasAxe && !GameController.getPausedStatus () && !GameController.getGameOver ()) {
			GUI.Box (new Rect (0, 60, 200, 25), "Press E to destroy the obstacle.");
		} else if (inTrigger && hasAxe && !GameController.getPausedStatus () && !GameController.getGameOver ()) {
			GUI.Box (new Rect (0, 60, 200, 25), "Find an axe to destroy the obstacle.");
		} 
	}
}
