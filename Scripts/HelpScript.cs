using UnityEngine;
using System.Collections;
public class HelpScript : MonoBehaviour {
	public GUIStyle style;
	public TextAsset help;
	private bool hide=true;
	private Rect windowRect=new Rect (Screen.width - Screen.width / 2 - 250, Screen.height - Screen.height / 2 - 150, 500, 400);

	void OnGUI(){
		//GUI.Box (new Rect (Screen.width - Screen.width / 2 - 250, Screen.height - Screen.height / 2 - 150, 500, 400), help.text);
		//GUI.color = Color.yellow;
		//windowRect=GUI.Window (0, windowRect, WindowFunction, "Help");
		if (hide != true) {
			GUI.color = Color.yellow;
			windowRect=GUI.Window (0, windowRect, WindowFunction, "Help");
		} 
	}

	private void WindowFunction(int id){

		GUI.Box (new Rect (0, 30, 500, 370),help.text);
		if (GUI.Button (new Rect (475, 5, 20, 20), "X")) {
			hide = true;
		}
		GUI.DragWindow ();
	}

	private void InitStyles(){
		if (style == null) {
			style = new GUIStyle (GUI.skin.box);
			//style.normal.background = MakeTex (2, 2, new Color (0f, 1f, 0f, 0.5f));
		}
	}

	public void setHide(){
		hide = false;
	}

}
