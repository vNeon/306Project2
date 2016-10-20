using UnityEngine;
using System.Collections;

/**
 * Custom Implementation for jumping
 **/ 
public class PlayerControllerMinigame : MonoBehaviour {

	public float jump;
	public float speed;
	private float moveVelocity;
	private bool isGrounded = true;


	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		// Jump
		if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W)){
			//GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity .x, jump);
			Rigidbody2D body = GetComponent<Rigidbody2D> ();
			body.AddForce (transform.up * jump);
			Debug.Log ("Adding Force: " + transform.up * jump);
		}
	}
}