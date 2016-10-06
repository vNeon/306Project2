using UnityEngine;
using System.Collections;

//Method to cause the player step through portals
public class StepThroughPortal : MonoBehaviour {

	public string lr;
	private GameObject otherPortal;
	private GameObject player;


	// Update is called once per frame
	//Make the portal links to other portal
	void Update () {
		player = GameObject.FindWithTag ("Player");
		if (lr == "left") {
			Debug.Log ("left");
			otherPortal = player.GetComponent<ThrowPortal> ().getLeftPortal ();

		} else if (lr == "right") {
			Debug.Log ("right");
			otherPortal = player.GetComponent<ThrowPortal> ().getRightPortal ();
		}
	}
	//When player enters the portal, the player transported to other portal.
	void OnTriggerEnter(Collider other){
		if (other.tag == "Player" || other.tag == "MainCamera" || other.tag == "PickupObject") {
			if (other.tag == "PickupObject") {
				other.GetComponent<Pickupable> ().dropObject ();
			}
			if (otherPortal != null) {
				other.transform.position = otherPortal.transform.position+otherPortal.transform.forward*3;
				//other.transform.rotation = rotation;
			}
		}
	}
}