using UnityEngine;
using System.Collections;
//To read the clue once the button icon in the inventory is clicked.
public class Read : MonoBehaviour {
	public bool inTrigger = false;
	protected TextAsset message;
	protected bool isOpen = false;
	public GameObject clue;

	//Method to read the clue
	public void read(){
		if (!isOpen) {
			inTrigger = true;
			message = clue.GetComponent<Interactable> ().message;
			isOpen = true;
		} else {
			inTrigger = false;
			isOpen = false;
		}


	}

	//Show the clue on the GUI
	void OnGUI()
	{
		if (inTrigger)
		{
			GUI.Box(new Rect(Screen.width - Screen.width / 2 - 125, Screen.height - Screen.height / 2 -150, 250, 300), message.text);
		}

	}
}
