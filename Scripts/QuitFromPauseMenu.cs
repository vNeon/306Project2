using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/**
 * Quit from the pause menu
 * */
public class QuitFromPauseMenu : MonoBehaviour {
	private bool hide=true;
	private Rect windowRect=new Rect (Screen.width - Screen.width / 2 - 150, Screen.height - Screen.height / 2 - 50, 300, 150);

	void OnGUI(){
        //make a gui window asking the user if they really want to quit
		if (hide != true) {
			GUI.color = Color.gray;
			windowRect=GUI.Window (0, windowRect, WindowFunction, "Quit");
		} 
	}
    //Settings for the gui window
	private void WindowFunction(int id){
		GUI.Label(new Rect(25, 30, 500, 30), "Progress will be lost. Do you want to quit?");
		if (GUI.Button (new Rect (270, 5, 20, 0), "X")) {
			hide = true;
		}
		if (GUI.Button (new Rect (45, 60, 100, 30), "Yes")) {
			hide = true;
			returnMainMenu ();
		}
		if (GUI.Button (new Rect (155, 60, 100, 30), "No")) {
			hide = true;
		}
	}

	public void setVisible(){
		hide = false;
	}
	private void returnMainMenu(){
        SceneManager.LoadScene(0);
	}

}
