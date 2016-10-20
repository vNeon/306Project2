using UnityEngine;
using System.Collections;

/*
 * This script controls the behaviour of doors.
 * A key object is associated with a door which when found
 * allows the player to open the door.
 */
public class Door : MonoBehaviour
{
    // public variables which can be changed within unity
    public bool inTrigger;
    public bool key;
    public GameController gm;
    public AudioClip doorOpenAudio;
    public AudioClip doorCloseAudio;

	protected bool isOpen = false;

    // Check if the player enters the doors collider
    void OnTriggerEnter(Collider other)
    {
        inTrigger = true;
    }

    // Check if the player leaves the doors collider
    void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Opens and closes the door if the player has a key
        if (inTrigger && key)
        {
			if (Input.GetKeyDown (KeyCode.E) && !isOpen)
            {
				this.GetComponent<Animation> ().Play ("DoorOpen");
				isOpen = true;
                AudioSource.PlayClipAtPoint(doorOpenAudio, transform.position, 0.5f);
                // Achievement for opening their first door
                if (!PlayerPrefs.HasKey("FirstDoor"))
                {
                    gm.UnlockAchievement("FirstDoor", "Open Sesame: Opened the first door.");
                }
			}
            else if (Input.GetKeyDown (KeyCode.E) && isOpen)
            {
				this.GetComponent<Animation> ().Play ("DoorClose");
				isOpen = false;
                AudioSource.PlayClipAtPoint(doorCloseAudio, transform.position, 0.5f); //improvement: add 1.5 sec delay to wait for animation to close
            }
        }
    }

    // Displays text which informs the player what interaction they 
    // can perform
    void OnGUI()
    {
        if (inTrigger && !GameController.getGameOver() && !GameController.getPausedStatus())
        {
			if (key && !isOpen)
            {
				GUI.Box (new Rect (0, 0, 200, 25), "Press E to open");
			}
            else if(key && isOpen) {
				GUI.Box (new Rect (0, 0, 200, 25), "Press E to close");
			}
            else
            {
                GUI.Box(new Rect(0, 0, 200, 25), "Need a key!");
            }
        }
    }
}
