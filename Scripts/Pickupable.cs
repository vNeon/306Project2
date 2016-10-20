using UnityEngine;
using System.Collections;

//Author: Jack Wong
//Create an object that could be picked up by the player
//attach this script to pickupable object
public class Pickupable : MonoBehaviour {
	GameObject mainCamera;
	GameObject player;
	bool carrying;
	GameObject carriedObject;
	public float range = 2;
	public float distance = 3;
	public float verticalDistance = 2;
	public float smooth=4;

    public AudioClip dropAudio;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindWithTag ("MainCamera");
		player = GameObject.FindWithTag ("Player");
	}

	// Update is called once per frame
	void Update () {
		if (carrying) {
			carry (carriedObject);
			checkDrop ();
		} else {
			pickup ();
		}
	}

	//Method to carry the object
	void carry(GameObject o){
		Vector3 vector = new Vector3 (0, verticalDistance,0);
		o.transform.position = Vector3.Lerp(o.transform.position, player.transform.position + vector+player.transform.forward *distance,Time.deltaTime*smooth);
	}

	//Method to pick up the object
	void pickup(){
		//When player left click
		if (Input.GetMouseButton(0)) {

			//Middle of the screen(crosshair)
			int x = Screen.width / 2;
			int y = Screen.height / 2;
			Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay (new Vector3(x,y));

			//find the player position and object position
			Vector3 objPosition = this.transform.position;
			Vector3 playerPosition = player.transform.position;

			//Use ray cast to check if the object is in the range of the player
			RaycastHit hit;
			if (Physics.Raycast (ray,out hit) && Vector3.Distance(playerPosition,objPosition)<range) {
				Pickupable p = hit.collider.GetComponent<Pickupable> ();
				if (p != null) {
					carrying = true;
					carriedObject = p.gameObject;
					p.gameObject.GetComponent<Rigidbody>().isKinematic = false;
					p.gameObject.GetComponent<Rigidbody> ().useGravity = false;
					p.gameObject.GetComponent<Rigidbody> ().freezeRotation = true;
				}
			}
		}
	}

	//Check if the object is picked
	void checkDrop(){
		if (Input.GetMouseButtonDown (0)) {
			Debug.Log ("Clicked an object while its picked");
			dropObject ();
		}

	}
	//Player drop the object.
	public void dropObject (){
        if (carriedObject != null)
        {
            carrying = false;
            carriedObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            carriedObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
			carriedObject.gameObject.GetComponent<Rigidbody> ().freezeRotation = false;
            carriedObject = null;
        }
		
	}

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 2)
            AudioSource.PlayClipAtPoint(dropAudio, transform.position);
    }
}

