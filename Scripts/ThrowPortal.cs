using UnityEngine;
using System.Collections;

//Player are able to throw the portal
public class ThrowPortal : MonoBehaviour {
	public GameObject leftPortal;
	public GameObject rightPortal;
	private GameObject left;
	private GameObject right;

	//Check if the portal is first shoot
	private static int leftcounter = 0;
	private static int rightcounter = 0;
	GameObject mainCamera;

	// Use this for initialization
	//Initialise the counter
	void Start () {
		mainCamera = GameObject.FindWithTag ("MainCamera");
		Debug.Log (leftcounter);
		leftcounter = 0;
		rightcounter = 0;
	}

	// Update is called once per frame
	//Use mouse click and left shift to shoot portal
	//If the left portal or right portal is first shoot, instantiate the portal object.
	void Update () {

		if (Input.GetKey (KeyCode.LeftShift)) {
			if (Input.GetMouseButtonDown (0)) {
				Debug.Log ("Left");
				if (leftcounter == 0) {
					left = Instantiate (leftPortal);
					leftcounter++;
				}
				throwPortal (left);
			}else if (Input.GetMouseButtonDown(1)){
				Debug.Log ("Right");
				if (rightcounter == 0) {
					right = Instantiate (rightPortal);
					rightcounter++;
				}
				throwPortal (right);
			}
		}
	}

	//Throw the portal using raycast
	void throwPortal(GameObject portal){
		//Middle of the screen (crosshair)
		int x = Screen.width / 2;
		int y = Screen.height / 2;

		Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay (new Vector3 (x, y));
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {

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

	public void setLeftCounter(int counter){
		leftcounter = counter;
	}

	public void setRightCounter(int counter){
		rightcounter = counter;
	}
}

