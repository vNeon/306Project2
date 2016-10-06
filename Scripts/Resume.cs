using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Resume : MonoBehaviour {

	public GameObject gameController;

	public void DisableMenu() {

		gameController.GetComponent<GameController> ().ClosePauseCanvas ();

	}
}
