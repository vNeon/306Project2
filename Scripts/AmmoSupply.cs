using UnityEngine;
using System.Collections;

//Author: Jack Wong
//To provide ammo to the portal gun.
//attach this script to ammo crate to provide 2 ammo per object.
//Reused for beer interaction in lvl 4
public class AmmoSupply : MonoBehaviour {

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
    //When the player goes within range of the ammo, destroy it and give player ammo
	void OnTriggerEnter(Collider collider){
		if (collider.tag == "Player") {
			Destroy (this.gameObject);
            provideAmmo ();
		}

	}

	//add ammo to player
	public void provideAmmo(){
		player.GetComponent<ThrowPortal> ().addAmmo (2);
	}
}
