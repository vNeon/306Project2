using UnityEngine;
using System.Collections;

//Author: Jack Wong
//To destroy the existing portal after certain amount of time.
//Attack to the prefab of both portals
//default: 10sec
public class DestroyPortal : MonoBehaviour {
	private GameObject player;
	public float TimeToDestroy = 10;
	// Use this for initialization
	//Destroy the portal after certain time interval.
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		Invoke ("destroyPortal", TimeToDestroy);
	}
	
	//Destroy the portal and reset counter.
	void destroyPortal(){
		player.GetComponent<ThrowPortal> ().setLeftCounter (false);
		player.GetComponent<ThrowPortal> ().setRightCounter (false);
		Destroy (this.gameObject);
	}
}
