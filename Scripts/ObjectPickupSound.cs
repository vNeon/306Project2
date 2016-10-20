using UnityEngine;
using System.Collections;

/**
 * Play the audio when picking up boxes
 **/ 
public class ObjectPickupSound : MonoBehaviour {
    //Simple class for all story based objects.
    AudioClip effect;
	// Get the audio clip from inspector
	void Start () {
        effect = GetComponent<AudioSource>().clip;
	}
	
	// Plays the clip
	public void playEffect () {
        AudioSource.PlayClipAtPoint(effect, transform.position);
	}
}
