using UnityEngine;
using System.Collections;

//Author: Jack Wong
//Player are able to throw the portal. Check if the portal is in the same place.
//Player can shoot the portal based on distance and position of the throwing portal.
//Attach to player to throw the portal.
public class ThrowPortal : MonoBehaviour {
	public GameObject leftPortal;
	public GameObject rightPortal;
	public Texture2D backgroundTexture;

	private GameObject left;
	private GameObject right;
	private RaycastHit hit;
	private static int ammo;

	//The shooting distance 
	public float shootDistance = 100;

	//Check if the portal is first shoot
	private bool leftcounter = false;
	private bool rightcounter = false;
	GameObject mainCamera;

	// Use this for initialization
	//Initialise the counter
	void Start () {
		mainCamera = GameObject.FindWithTag ("MainCamera");

		ammo = 10;
		leftcounter = false;
		rightcounter = false;
		shootDistance = 100;
		Cursor.visible = false;
	}

	// Update is called once per frame
	//Use mouse click and left shift to shoot portal
	//If the left portal or right portal is first shoot, instantiate the portal object.
	void Update () {
		// check if ammo exist
		if (ammo != 0) {
			if (Input.GetKey (KeyCode.LeftShift)) {
				if (Input.GetMouseButtonDown (0)) {

					if (!leftcounter && left == null) {
						left = checkPos (leftPortal);


					}
					if (left != null) {
						leftcounter = true;
						throwPortal (left);
					}

				}else if (Input.GetMouseButtonDown(1)){

					if (!rightcounter && right == null) {
						right = checkPos (rightPortal);


					}
					if (right != null) {
						rightcounter = true;
						throwPortal (right);
					}
				}
			}
		}
	}

	//Check if the throwing portal is in the same place as the existing portal
	//If true, do not instantiate the portal.
	GameObject checkPos(GameObject portal){
		int x = Screen.width / 2;
		int y = Screen.height / 2;
		GameObject[] portals = GameObject.FindGameObjectsWithTag ("Portal");

		bool canShoot = true;

		Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay (new Vector3 (x, y));

		if (Physics.Raycast (ray, out hit, shootDistance)) {
			foreach (GameObject p in portals) {
				if (hit.collider.gameObject == p) {
					canShoot = false;
					return null;
				}

			}
			ammo-=1;
			return Instantiate (portal);
		} else {
			canShoot = false;
			return null;
		}


	}
	//Throw the portal using raycast
	void throwPortal(GameObject portal){
		//Middle of the screen (crosshair)
		int x = Screen.width / 2;
		int y = Screen.height / 2;
		GameObject[] portals = GameObject.FindGameObjectsWithTag ("Portal");



		bool canShoot = true;

		Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay (new Vector3 (x, y));

		//Check the shooting distance
		if (Physics.Raycast (ray, out hit,shootDistance)) {
			
			foreach (GameObject p in portals) {
				if (hit.collider.gameObject == p) {
					canShoot = false;

				}

			}

		} else {
			
			canShoot = false;
		}
		if (canShoot) {
			//Rotate the portal to fit the terrain
			Quaternion hitObjectRotation = Quaternion.LookRotation (hit.normal);
			portal.transform.position = hit.point;
			portal.transform.rotation = hitObjectRotation;
		}

	}

	//setter and getter methods.
	public GameObject getRightPortal(){
		return right;
	}
	public GameObject getLeftPortal(){
		return left;
	}

	public void setLeftCounter(bool leftcounter){
		this.leftcounter = leftcounter;
	}
	public void setRightCounter(bool rightcounter){
		this.rightcounter = rightcounter;
	}
	//Get the ammo
	public void addAmmo(int numAmmo){
		ammo += numAmmo;
	}
	//display count of ammo
	void OnGUI(){
		GUIStyle style = new GUIStyle("box");
		style.fontSize = 20;
		style.normal.textColor = Color.black;
		style.fontStyle = FontStyle.Bold;
		style.normal.background = backgroundTexture;
		GUI.Box(new Rect(0,Screen.height/2, 200, 35), "Portal ammo: "+ammo,style);

	}
}

