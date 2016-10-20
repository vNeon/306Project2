using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/**
 * Resume to the game
 **/ 
public class Resume : MonoBehaviour {

	public GameObject gameController;

    //Called when the resume button in the pause menu is clicked.
	public void DisableMenu() {

		gameController.GetComponent<GameController> ().ClosePauseCanvas ();

	}
}
