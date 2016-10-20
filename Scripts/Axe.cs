using UnityEngine;
using System.Collections;

// Describes the behaviour of axe used in level 3. 
// Priyankit Singh
public class Axe : MonoBehaviour {
	public bool inTrigger;
	public GameObject gamecontroller;
	public GameObject inventoryPanel;
	public GameObject[] inventoryIcons;
    public AudioClip sound;

	// Check if the player enters the keys collider.
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			inTrigger = true;
		}

	}

	// Check if the player enters the keys collider.
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") {
			inTrigger = false;
		}
	}

	// Update is called once per frame
	void Update () {
		if (inTrigger && !GameController.getPausedStatus())
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				//Play axe sound
                AudioSource.PlayClipAtPoint(sound, transform.position, 0.9f);
                
                // Set the hasAxe on GameController to true
				gamecontroller.GetComponent<GameController>().setAxeTrue();
				Destroy(this.gameObject);

				// Add key to inventory.
				GameObject i = Instantiate (inventoryIcons[0]);
				// i.transform.SetParent (inventoryPanel.transform);
				i.transform.parent = inventoryPanel.transform;
				gamecontroller.GetComponent<GameController> ().setAxeTrue ();
                
			}
		}
	}

	// Displays text to player telling them how to pick up the key.
	void OnGUI()
	{
		if (inTrigger && !GameController.getPausedStatus() && !GameController.getGameOver())
		{
			GUI.Box(new Rect(0, 60, 200, 25), "Press E to pick up axe");
		}
	}
}
