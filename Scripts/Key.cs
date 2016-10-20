using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/* This script controls the behaviour of keys.
 * This object will unlock doors when they are picked up
 * by the player.
 */
public class Key : MonoBehaviour {
    // public variables which can be changed within unity
    public bool inTrigger;
	public GameObject door;
	public GameObject inventoryPanel;
	public GameObject[] inventoryIcons;

    public AudioClip keyRattle;

    void Start() {
        keyRattle = GetComponent<AudioSource>().clip;
        //reset variables
        door.GetComponent<Door>().key = false;
    }
    // Check if the player enters the keys collider.
    void OnTriggerEnter(Collider other)
    {
        inTrigger = true;
    }

    // Check if the player enters the keys collider.
    void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }

    // Update is called once per frame.
    void Update()
    {
		if (inTrigger && !GameController.getPausedStatus())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Play key rattle sound
                //Debug.Log(keyRattle.name);
                AudioSource.PlayClipAtPoint(keyRattle, transform.position);

                // Makes a door avaiable for use by the payer.
				door.GetComponent<Door> ().key = true;
                Destroy(this.gameObject);

                // Add key to inventory.
				GameObject i = Instantiate (inventoryIcons[0]);
				// i.transform.SetParent (inventoryPanel.transform);
				i.transform.parent = inventoryPanel.transform;
            }
        }
    }

    // Displays text to player telling them how to pick up the key.
    void OnGUI()
    {
		if (inTrigger && !GameController.getPausedStatus() && !GameController.getGameOver())
        {
            GUI.Box(new Rect(0, 60, 200, 25), "Press E to take key");
        }
    }
}
