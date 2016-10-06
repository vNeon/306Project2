using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class QuitFromPauseMenu : MonoBehaviour {
	private bool hide=true;
	private Rect windowRect=new Rect (Screen.width - Screen.width / 2 - 150, Screen.height - Screen.height / 2 - 50, 300, 150);

	void OnGUI(){
		if (hide != true) {
			GUI.color = Color.gray;
			windowRect=GUI.Window (0, windowRect, WindowFunction, "Quit");
		} 
	}
	private void WindowFunction(int id){

		if (GUI.Button (new Rect (270, 5, 20, 20), "X")) {
			hide = true;
		}
		if (GUI.Button (new Rect (45, 60, 100, 30), "Save")) {
			hide = true;
			returnMainMenu ();
		}
		if (GUI.Button (new Rect (155, 60, 100, 30), "Don't Save")) {
			hide = true;
			returnMainMenu ();
		}
	}

	public void setVisible(){
		hide = false;
	}
	private void returnMainMenu(){
		SceneManager.LoadScene(0);
	}

}
