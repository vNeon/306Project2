using UnityEngine;
using System.Collections;

/**
 * Sound effect volume control
 **/ 
public class SoundEffectVolume : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AudioListener.volume = PlayerPrefs.GetFloat("SoundEffectsVolume");
	}
	
	// Updates sound effect volume, used by sound controller
	public void UpdateVolume () {
        AudioListener.volume = PlayerPrefs.GetFloat("SoundEffectsVolume");
	}
}
