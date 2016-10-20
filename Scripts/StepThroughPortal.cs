using UnityEngine;
using System.Collections;

//Author: Jack Wong
//Method to cause the player step through portals
public class StepThroughPortal : MonoBehaviour {

	public string lr;
	private GameObject otherPortal;
	private GameObject player;
    private AudioClip sound;

    void Start() {
        sound = GetComponent<AudioSource>().clip;
    }
	// Update is called once per frame
	//Make the portal links to other portal
	void Update () {
		player = GameObject.FindWithTag ("Player");
		if (lr == "left") {

			otherPortal = player.GetComponent<ThrowPortal> ().getLeftPortal ();

		} else if (lr == "right") {

			otherPortal = player.GetComponent<ThrowPortal> ().getRightPortal ();
		}
	}
	//When player enters the portal, the player transported to other portal.
	void OnTriggerEnter(Collider other){
		if (other.tag == "Player" || other.tag == "MainCamera" || other.tag == "PickupObject"||other.tag == "NPC") {
			if (other.tag == "PickupObject") {
				other.GetComponent<Pickupable> ().dropObject ();
			}
			if (otherPortal != null) {
				//Vector3 velocity = other.GetComponent<Rigidbody> ().velocity;

				float mass = other.GetComponent<Rigidbody>().mass;
				float speed = other.GetComponent<Rigidbody>().velocity.magnitude;
				float force = mass * speed;
				//Debug.Log ("The speed is " + speed);
				//Debug.Log ("The force is " + force);
				Vector3 dir = otherPortal.transform.forward;
				//Debug.Log ("The original dir is " + dir);
				transform.Translate (dir * Time.deltaTime, Space.World);
				//Debug.Log ("The direction is " + dir);
				//Debug.Log ("The forward dir is " + otherPortal.transform.forward);
				other.transform.position = otherPortal.transform.position+otherPortal.transform.forward*3;

				//other.GetComponent<Rigidbody> ().AddForce (otherPortal.transform.forward.x * velocity);
				//Debug.Log("The final dir is "+(otherPortal.transform.up + otherPortal.transform.forward));
				//other.transform.rotation = rotation;
				other.GetComponent<Rigidbody>().velocity = Vector3.zero;
				other.GetComponent<Rigidbody>().AddForce((otherPortal.transform.up+otherPortal.transform.forward)*force*20);
				//Debug.Log("The final velocity is "+(otherPortal.transform.up+otherPortal.transform.forward)*force*20);
                
                //Play blink sound
                AudioSource.PlayClipAtPoint(sound, otherPortal.transform.position);
			}
		}
	}
}
